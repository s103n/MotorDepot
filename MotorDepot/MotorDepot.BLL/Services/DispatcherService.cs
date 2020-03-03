using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
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

        public async Task<OperationStatus<UserDto>> CreateDispatcher(UserDto userDto)
        {
            if (userDto == null)
                return new OperationStatus<UserDto>(
                    "User is empty",
                    HttpStatusCode.BadRequest,
                    null);

            var sameUser = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if (sameUser != null)
                return new OperationStatus<UserDto>(
                    "User with same e-mail address is exists",
                    HttpStatusCode.BadRequest,
                    userDto);

            var user = userDto.ToAppUser();
            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (!status.Succeeded)
                return new OperationStatus<UserDto>(
                    status.Errors.First(),
                    HttpStatusCode.BadRequest,
                    userDto);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus<UserDto>(
                "Registration was being successful",
                HttpStatusCode.OK,
                userDto);
        }

        public OperationStatus<IEnumerable<UserDto>> GetDispatchers()
        {
            return new OperationStatus<IEnumerable<UserDto>>(
                "Ok",
                HttpStatusCode.OK,
                _database.UserManager.Users
                .Where(user => _database.UserManager.IsInRole(user.Id, "dispatcher"))
                .AsEnumerable()
                .ToUserDtos());
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
