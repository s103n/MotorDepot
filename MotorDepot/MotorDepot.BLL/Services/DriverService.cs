using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Services
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _database;

        public DriverService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateDriver(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var sameUser = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (sameUser != null)
                return new OperationStatus("User with same e-mail address is exists", false);

            var user = userDto.ToEntity();
            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus(status.Errors.First(), false);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("Driver was successfully created", true);
        }

        public async Task<IEnumerable<UserDto>> GetDriversAsync()
        {
            var users = (await _database.UserManager.Users.ToListAsync())
                .Where(user => _database.UserManager.IsInRole(user.Id, "driver"))
                .ToDto();

            return users;
        }

        public async Task<OperationStatus> SendFlightRequest(FlightRequestDto flightRequest)
        {
            if (flightRequest == null)
                throw new ArgumentNullException(nameof(flightRequest));

            await _database.FlightRequestRepository.AddAsync(flightRequest.ToEntity());

            return new OperationStatus("Flight request was sent", true);
        }

        public async Task<OperationStatus<UserDto>> GetDriverById(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var user = await _database.UserManager.FindByIdAsync(id);

            if(user == null)
                return new OperationStatus<UserDto>("Driver doesn't exist.", HttpStatusCode.BadRequest, false);

            return new OperationStatus<UserDto>("", user.ToUserDto(), true);
        }

        public async Task<OperationStatus<IEnumerable<FlightDto>>> GetFlightsByDriver(string driverId)
        {
            if(driverId == null)
                throw new ArgumentNullException(nameof(driverId));

            var driver = await _database.UserManager.FindByIdAsync(driverId);

            if(driver == null)
                return new OperationStatus<IEnumerable<FlightDto>>("Driver doesn't exist", HttpStatusCode.NotFound, false);

            var flights = (await _database.FlightRepository.GetAllAsync())
                .Where(flight => flight.Driver != null && flight.Driver.Id == driverId);

            return new OperationStatus<IEnumerable<FlightDto>>("", flights.ToDto(), true);
        }

        public async Task<OperationStatus<FlightDto>> CurrentFlight(string driverId)
        {
            if(driverId == null)
                throw new ArgumentNullException(nameof(driverId));

            var driver = await _database.UserManager.FindByIdAsync(driverId);

            if(driver == null)
                return new OperationStatus<FlightDto>("Driver doesn't exist", HttpStatusCode.NotFound, false);

            var currentFlight = (await _database.FlightRepository.GetAllAsync())
                .FirstOrDefault(flight => flight.DriverId != null 
                                          && flight.Driver.Id == driverId 
                                          && flight.Status.Id == FlightStatus.Occupied
                                          || flight.Status.Id == FlightStatus.Performed);/////

            if(currentFlight == null)
                return new OperationStatus<FlightDto>("", null, true);

            return new OperationStatus<FlightDto>("", currentFlight.ToDto(), true);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
