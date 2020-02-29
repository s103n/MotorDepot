using Microsoft.AspNet.Identity;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
                return await _database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

            return null;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
