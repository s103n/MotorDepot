using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
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
                return new OperationStatus("Email", "User with same e-mail address is exists", false);

            var user = userDto.ToAppUser();
            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus("", status.Errors.First(), false);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("", "Registration was being successful", true);
        }

        public IEnumerable<UserDto> GetDrivers()
        {
            return _database.UserManager.Users
                .Where(user => _database.UserManager.IsInRole(user.Id, "driver"))
                .AsEnumerable()
                .ToUserDtos();
        }

        public async Task SendFlightRequest(FlightRequestDto flightRequest)
        {
            if(flightRequest == null)
                throw new ArgumentNullException(nameof(flightRequest));

            await _database.FlightRequestRepository.AddAsync(flightRequest.ToFlightRequest());
        }

        public async Task<UserDto> GetDriverById(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            var user = await _database.UserManager.FindByIdAsync(id);

            return user.ToUserDto();
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
