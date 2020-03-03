using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities.Enums;
using MotorDepot.DAL.Interfaces;
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

        public async Task<OperationStatus> SetRequestStatus(
            int? requestId, 
            string creatorId, 
            FlightRequestStatus status)
        {
            if (requestId == null)
                return new OperationStatus("Id is empty", HttpStatusCode.BadRequest);

            var request = await _database.FlightRequestRepository.FindAsync(requestId);

            if (request == null)
                return new OperationStatus("Request doesn't exist", HttpStatusCode.NotFound);

            request.FlightRequestStatusId = (FlightRequestStatusEnum)status;
            request.DispatcherId = creatorId;

            await _database.FlightRequestRepository.UpdateAsync(request);

            return new OperationStatus("Ok", HttpStatusCode.OK);
        }

        public async Task<OperationStatus<FlightRequestDto>> GetRequestByIdAsync(int? requestId)
        {
            if (requestId == null)
                return new OperationStatus<FlightRequestDto>("Id is empty", HttpStatusCode.BadRequest, null);

            var request = await _database.FlightRequestRepository.FindAsync(requestId);

            if (request == null)
                return new OperationStatus<FlightRequestDto>(
                    "Flight request does not found",
                    HttpStatusCode.NotFound,
                    null);

            return new OperationStatus<FlightRequestDto>(
                "Ok",
                HttpStatusCode.OK,
                request.ToFlightRequestDto());
        }

        public async Task<OperationStatus<IEnumerable<FlightRequestDto>>> GetFlightRequestsAsync()
        {
            var requests = await _database.FlightRequestRepository.GetAllAsync();
            return new OperationStatus<IEnumerable<FlightRequestDto>>(
                "Ok",
                HttpStatusCode.OK,
                requests.ToFlightRequestDto());
        }
    }
}
