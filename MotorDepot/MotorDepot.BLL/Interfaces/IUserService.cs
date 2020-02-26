using System;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationStatus> CreateAsync(UserDto userDto);
    }
}
