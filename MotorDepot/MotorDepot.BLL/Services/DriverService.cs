using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
                return new OperationStatus(
                    "User with same e-mail address is exists", 
                    HttpStatusCode.BadRequest);

            var user = userDto.ToAppUser();
            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus(status.Errors.First(), HttpStatusCode.BadRequest);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("Registration was being successful", HttpStatusCode.OK);
        }

        public OperationStatus<IEnumerable<UserDto>> GetDrivers()
        {
            return new OperationStatus<IEnumerable<UserDto>>(
                "Drivers",
                HttpStatusCode.OK,
                _database.UserManager.Users
                .Where(user => _database.UserManager.IsInRole(user.Id, "driver"))
                .AsEnumerable()
                .ToUserDtos());
        }

        public async Task<OperationStatus> SendFlightRequest(FlightRequestDto flightRequest)
        {
            if (flightRequest == null)
                return new OperationStatus("Flight request is null", HttpStatusCode.BadRequest);

            await _database.FlightRequestRepository.AddAsync(flightRequest.ToFlightRequest());

            return new OperationStatus(
                $"Request for flight #{flightRequest.RequestedFlight.Id} was sent", 
                HttpStatusCode.OK);
        }

        public async Task<OperationStatus<UserDto>> GetDriverById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new OperationStatus<UserDto>("Id is empty", HttpStatusCode.BadRequest, null);

            var user = await _database.UserManager.FindByIdAsync(id);

            return new OperationStatus<UserDto>("Driver received", HttpStatusCode.OK, user.ToUserDto());
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
