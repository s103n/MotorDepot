using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDispatcherService : IDisposable
    {
        /// <summary>
        /// Creating new dispatcher
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="userDto">User dto object</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> CreateDispatcher(UserDto userDto);
        /// <summary>
        /// Getting all dispatchers
        /// </summary>
        /// <returns>IEnumerable object of user dto object where role is dispatcher</returns>
        IEnumerable<UserDto> GetDispatchers();
    }
}
