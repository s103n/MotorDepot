using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDispatcherService : IDisposable
    {
        Task<OperationStatus<UserDto>> CreateDispatcher(UserDto userDto);
        OperationStatus<IEnumerable<UserDto>> GetDispatchers();
    }
}
