using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities.Enums;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

            request.FlightRequestStatusId = (FlightRequestStatusEnum)status;
            request.DispatcherId = creatorId;

            await _database.FlightRequestRepository.UpdateAsync(request);

            return new OperationStatus("", true);
        }

        public async Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId)
        {
            if (requestId == null)
                throw new ArgumentNullException(nameof(requestId));

            var request = await _database.FlightRequestRepository.FindAsync(requestId);

            if (request == null)
                return new OperationStatus<FlightRequestDto>("Request doesn't exist", HttpStatusCode.NotFound, false);

            return new OperationStatus<FlightRequestDto>("", request.ToDto(), true);
        }

        public async Task<OperationStatus<IEnumerable<FlightRequestDto>>> GetFlightRequestsAsync()
        {
            var requests = await _database.FlightRequestRepository.GetAllAsync();

            return new OperationStatus<IEnumerable<FlightRequestDto>>("", requests.ToDto(), true);
        }
    }
}
