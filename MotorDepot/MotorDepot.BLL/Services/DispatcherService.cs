﻿using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Services
{
    public class DispatcherService : IDispatcherService
    {
        private readonly IUnitOfWork _database;

        public DispatcherService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateDispatcher(UserDto userDto)
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

        public IEnumerable<UserDto> GetDispatchers()
        {
            return _database.UserManager.Users
                .ToList()
                .Where(user => _database.UserManager.IsInRole(user.Id, "dispatcher"))
                .ToList()
                .ToUserDtos();
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}