using MotorDepot.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Interfaces
{
    public interface IFlightRequestService : IDisposable
    {
        Task<OperationStatus> ConfirmRequest(int requestId, string creatorId, FlightRequestStatus status);
        Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId);
        Task<OperationStatus<IEnumerable<FlightRequestDto>>> GetFlightRequestsAsync(FlightRequestStatus status);
    }
}
