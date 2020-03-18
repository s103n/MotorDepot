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
                return new OperationStatus("Dispatcher with same e-mail address is already exists", false);

            var user = userDto.ToEntity();
            var status = await _database.UserManager.CreateAsync(userDto.ToEntity(), userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus(status.Errors.First(), false);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("Registration was being successful", true);
        }

        public async Task<IEnumerable<UserDto>> GetDispatchers()
        {
            var dispatchers = (await _database.UserManager.Users.ToListAsync())
                .Where(user => _database.UserManager.IsInRole(user.Id, "dispatcher"))
                .ToDto();

            return dispatchers;
        }

        public async Task<OperationStatus<UserDto>> GetDispatcherAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
                return new OperationStatus<UserDto>("id is empty", HttpStatusCode.NotFound, false);

            var dispatcher = (await GetDispatchers()).FirstOrDefault(dis => dis.Id == id);

            if(dispatcher == null)
                return new OperationStatus<UserDto>("Dispatcher does not exist", HttpStatusCode.NotFound, false);

            return new OperationStatus<UserDto>("Ok", dispatcher, true);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
