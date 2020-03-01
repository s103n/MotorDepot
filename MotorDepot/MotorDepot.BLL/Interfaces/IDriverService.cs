using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDriverService : IDisposable
    {
        Task<OperationStatus> CreateDriver(UserDto userDto);
        IEnumerable<UserDto> GetDrivers();
        Task SendFlightRequest(FlightRequestDto flightRequest);
        Task<UserDto> GetDriverById(string id);
    }
}
