using System;
using System.Linq;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;

namespace MotorDepot.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateAsync(UserDto userDto)
        {
            if(userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var sameUser = await _database.UserManager.FindByEmailAsync(userDto.Email);

            if(sameUser != null)
                return new OperationStatus("Email", "User with same e-mail address is exists", false);

            var user = new AppUser
            {
                UserName = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                LastSignUp = DateTime.Now
            };

            var status = await _database.UserManager.CreateAsync(user, userDto.Password);

            if (status.Errors.Any())
                return new OperationStatus("", /*"O-ops something happened"*/ status.Errors.FirstOrDefault(), false);

            await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
            await _database.SaveAsync();

            return new OperationStatus("", "Registration was being successful", true);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
