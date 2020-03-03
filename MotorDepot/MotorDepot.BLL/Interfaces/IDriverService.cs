using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDriverService : IDisposable
    {
        Task<OperationStatus> CreateDriver(UserDto driver);
        OperationStatus<IEnumerable<UserDto>> GetDrivers();
        Task<OperationStatus> SendFlightRequest(FlightRequestDto flightRequest);
        Task<OperationStatus<UserDto>> GetDriverById(string id);
    }
}
