using MotorDepot.BLL.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;

namespace MotorDepot.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationStatus<ClaimsIdentity>> Authenticate(UserDto userDto);
    }
}
