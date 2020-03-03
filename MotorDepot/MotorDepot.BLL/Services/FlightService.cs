using MotorDepot.BLL.BusinessModels;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Enums;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;
using FlightStatus = MotorDepot.BLL.Infrastructure.Enums.FlightStatus;

namespace MotorDepot.BLL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _database;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateAsync(FlightDto flightDto)
        {
            if(flightDto == null)
                return new OperationStatus("Flight is null", HttpStatusCode.BadRequest);

            var flight = flightDto.ToFlight();

            await _database.FlightRepository.AddAsync(flight);

            return new OperationStatus("Ok", HttpStatusCode.OK);
        }

        public async Task<OperationStatus> EditAsync(FlightDto flightDto)
        {
            if(flightDto == null)
                return new OperationStatus("Flight is null", HttpStatusCode.BadRequest);

            await _database.FlightRepository.UpdateAsync(flightDto.ToFlight());

            return new OperationStatus("Ok", HttpStatusCode.OK);
        }

        public async Task<OperationStatus<FlightDto>> GetAsync(object property)
        {
            if(property == null)
                return new OperationStatus<FlightDto>("Properties don't exist", HttpStatusCode.BadRequest, null);

            var refGet = new ReflectionGet<Flight>(property);
            var item = await refGet.GetItemAsync(_database.FlightRepository);

            return new OperationStatus<FlightDto>("Ok", HttpStatusCode.OK, item.ToFlightDto());
        }

        public async Task<OperationStatus> SetStatus(FlightStatus status, int? flightId)
        {
            var flight = await _database.FlightRepository.FindAsync(flightId);

            if(flight == null)
                return new OperationStatus("Flight is not exists", HttpStatusCode.NotFound);

            flight.StatusId = (FlightStatusEnum)status;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus("Ok", HttpStatusCode.OK);
        }

        public async Task<OperationStatus<IEnumerable<FlightDto>>> GetAllAsync(bool deleted = false)
        {
            var flights = await _database.FlightRepository.GetAllAsync();

            if (deleted)
            {
                return new OperationStatus<IEnumerable<FlightDto>>(
                    "Ok",
                    HttpStatusCode.OK,
                    flights.ToFlightDtos());
            }

            return new OperationStatus<IEnumerable<FlightDto>>(
                "Ok", 
                HttpStatusCode.OK, 
                flights.Where(flight => flight.StatusId != FlightStatusEnum.Deleted)
                .ToList()
                .ToFlightDtos());
        }

        public async Task<OperationStatus<FlightDto>> GetByIdAsync(int? id)
        {
            if (id == null)
                return new OperationStatus<FlightDto>("Id is empty", HttpStatusCode.BadRequest, null);

            var flight = await _database.FlightRepository.FindAsync(id);

            if(flight == null)
                return new OperationStatus<FlightDto>("Flight does not exist", HttpStatusCode.BadRequest, null);

            return new OperationStatus<FlightDto>("Ok", HttpStatusCode.OK, flight.ToFlightDto());
        }

        public async Task<OperationStatus> RemoveAsync(int? id)
        {
            if (id == null)
                return new OperationStatus("Id is empty", HttpStatusCode.BadRequest);

            var flight = await _database.FlightRepository.FindAsync(id);

            if (flight == null)
                return new OperationStatus("Flight does not exist", HttpStatusCode.BadRequest);

            flight.StatusId = FlightStatusEnum.Deleted;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus("Ok", HttpStatusCode.OK);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
