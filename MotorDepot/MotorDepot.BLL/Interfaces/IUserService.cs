using MotorDepot.BLL.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;

namespace MotorDepot.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userDto">User dto object</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<ClaimsIdentity>> Authenticate(UserDto userDto);
    }
}
