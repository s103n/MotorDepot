using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;

namespace MotorDepot.BLL.Interfaces
{
    public interface IFlightService : IDisposable
    {
        Task<OperationStatus<IEnumerable<FlightDto>>> GetAllAsync(bool deleted = false);
        Task<OperationStatus> CreateAsync(FlightDto flightDto);
        Task<OperationStatus> RemoveAsync(int? id);
        Task<OperationStatus> EditAsync(FlightDto flightDto);
        Task<OperationStatus<FlightDto>> GetByIdAsync(int? id);
        Task<OperationStatus<FlightDto>> GetAsync(object property);
        Task<OperationStatus> SetStatus(FlightStatus status, int? flightId);
    }
}
