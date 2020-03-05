using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Services
{
    public class FlightRequestService : IFlightRequestService
    {
        private readonly IUnitOfWork _database;

        public FlightRequestService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public async Task<OperationStatus> ConfirmRequest(
            int requestId,
            string creatorId,
            FlightRequestStatus status)
        {
            var request = await _database.FlightRequestRepository.FindAsync(requestId);
            var dispatcher = await _database.UserManager.FindByIdAsync(creatorId);

            if (request == null || dispatcher == null)
                return new OperationStatus("Request or dispatcher doesn't exist", HttpStatusCode.NotFound, false);

            //if dispatcher set status of request like accepted, then other requests
            //that refer on the same flight will be canceled
            if (status == FlightRequestStatus.Accepted)
            {
                var requests = (await _database.FlightRequestRepository.GetAllAsync())
                    .ToList()
                    .Where(req => req.RequestedFlight.Id == request.FlightId);

                foreach (var req in requests)
                {
                    req.FlightRequestStatusLookupId = FlightRequestStatus.Canceled;

                    await _database.FlightRequestRepository.UpdateAsync(req);
                }
            }

            request.FlightRequestStatusLookupId = status;
            request.DispatcherId = creatorId;

            await _database.FlightRequestRepository.UpdateAsync(request);

            return new OperationStatus("Request(s) was updated", true);
        }

        public async Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId)
        {
            if (requestId == null)
                throw new ArgumentNullException(nameof(requestId));

            var request = await _database.FlightRequestRepository.FindAsync(requestId);

            if (request == null)
                return new OperationStatus<FlightRequestDto>("Request doesn't exist", HttpStatusCode.NotFound, false);

            if (request.Status.Id != (int)FlightRequestStatus.InQueue)
                return new OperationStatus<FlightRequestDto>("Requests can be accepted if they are in queue",
                    HttpStatusCode.BadRequest,
                    false);

            return new OperationStatus<FlightRequestDto>("", request.ToDto(), true);
        }

        public async Task<OperationStatus<IEnumerable<FlightRequestDto>>> GetFlightRequestsAsync(FlightRequestStatus status)
        {
            var requests = (await _database.FlightRequestRepository.GetAllAsync())
                .ToList()
                .Where(req => (int)req.Status.Id == (int)status
                              && req.RequestedFlight.FlightStatusLookupId == FlightStatus.Free);

            return new OperationStatus<IEnumerable<FlightRequestDto>>("", requests.ToDto(), true);
        }
    }
}
