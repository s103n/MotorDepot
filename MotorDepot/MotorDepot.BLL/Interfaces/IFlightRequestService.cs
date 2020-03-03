using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.BLL.Models;

namespace MotorDepot.BLL.Interfaces
{
    public interface IFlightRequestService : IDisposable
    {
        Task<OperationStatus> SetRequestStatus(int? requestId, string creatorId, FlightRequestStatus status);
        Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId);
        Task<OperationStatus<IEnumerable<FlightRequestDto>>> GetFlightRequestsAsync();
    }
}
