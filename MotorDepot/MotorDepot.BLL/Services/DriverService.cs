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
                return new OperationStatus("User with same e-mail address is exists", false);

            var user = userDto.ToEntity();
            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus(status.Errors.First(), false);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("Registration was being successful", HttpStatusCode.OK, true);
        }

        public OperationStatus<IEnumerable<UserDto>> GetDrivers()
        {
            var users = _database.UserManager.Users
                .Where(user => _database.UserManager.IsInRole(user.Id, "driver"))
                .AsEnumerable()
                .ToDto();

            return new OperationStatus<IEnumerable<UserDto>>("", users, true);
        }

        public async Task<OperationStatus> SendFlightRequest(FlightRequestDto flightRequest)
        {
            if (flightRequest == null)
                throw new ArgumentNullException(nameof(flightRequest));

            await _database.FlightRequestRepository.AddAsync(flightRequest.ToEntity());

            return new OperationStatus("", true);
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

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
