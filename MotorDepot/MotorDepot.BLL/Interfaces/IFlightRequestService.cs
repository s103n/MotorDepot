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
        /// <summary>
        /// Confirming request for flight
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="requestId">Id of request</param>
        /// <param name="creatorId">Id of creator</param>
        /// <param name="status">Status of flight request canceled or accepted</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> ConfirmRequestAsync(int requestId, string creatorId, FlightRequestStatus status);
        /// <summary>
        /// Getting flight request by id property
        /// </summary>
        /// <param name="requestId">Id of request</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId);
        /// <summary>
        /// Getting flight request by flight request status
        /// </summary>
        /// <param name="status">Status of flight request</param>
        /// <returns>IEnumerable of flight requests objects</returns>
        Task<IEnumerable<FlightRequestDto>> GetFlightRequestsAsync(FlightRequestStatus? status = null);
    }
}
