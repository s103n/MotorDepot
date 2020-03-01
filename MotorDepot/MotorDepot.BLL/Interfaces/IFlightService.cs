using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IFlightService : IDisposable
    {
        IEnumerable<FlightDto> GetAll(bool deleted = false);
        Task CreateAsync(FlightDto flightDto);
        Task RemoveAsync(int? id);
        Task EditAsync(FlightDto flightDto);
        Task<FlightDto> GetByIdAsync(int? id);
        Task<FlightDto> GetAsync(object property);
        Task SetStatus(FlightStatus status, int? flightId);
    }
}
