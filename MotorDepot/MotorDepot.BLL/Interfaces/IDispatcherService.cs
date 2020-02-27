using System;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDispatcherService : IDisposable
    {
        Task<OperationStatus> CreateDispatcher(UserDto userDto);
        IEnumerable<UserDto> GetDispatchers();
    }
}
