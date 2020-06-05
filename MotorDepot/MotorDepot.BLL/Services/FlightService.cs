using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MotorDepot.BLL.BusinessModels;
using MotorDepot.Shared;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _database;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateFlightAsync(FlightDto flightDto)
        {
            if (flightDto == null)
                throw new ArgumentNullException(nameof(flightDto));

            flightDto.Status = FlightStatus.Free;
            var flight = flightDto.ToEntity();

            await _database.FlightRepository.AddAsync(flight);

            return new OperationStatus("New flight was successfully created", true);
        }

        public async Task<OperationStatus> EditFlightAsync(FlightDto flightDto)
        {
            if (flightDto == null)
                throw new ArgumentNullException(nameof(flightDto));

            var flight = (await _database.FlightRepository.FindAsync(flightDto.Id))
                .ToDto()
                .ToEditWith(flightDto);
    
            await _database.FlightRepository.UpdateAsync(flight.ToEntity());

            return new OperationStatus($"Flight #{flightDto.Id} was updated", true);
        }

        public async Task<OperationStatus> SetStatus(FlightStatus status, int? flightId, bool cancelByDriver = false)
        {
            if (flightId == null)
                throw new ArgumentNullException(nameof(flightId));

            var flight = await _database.FlightRepository.FindAsync(flightId);

            if (flight == null)
                return new OperationStatus("Flight doesn't exist", HttpStatusCode.NotFound, false);

            flight.FlightStatusLookupId = status;

            if(cancelByDriver)
            {
                flight.AutoId = null;
                flight.DriverId = null;
            }

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus("Flight's status was updated", true);
        }

        public async Task<OperationStatus> SetDriverWithAuto(int flightId, int? autoId, string driverEmail)
        {
            var flight = await _database.FlightRepository.FindAsync(flightId);
            var driver = await _database.UserManager.FindByEmailAsync(driverEmail);

            if (flight == null || driver == null)
                return new OperationStatus("Flight or driver doesn't exist", HttpStatusCode.InternalServerError, false);

            flight.AutoId = autoId;
            flight.DriverId = driver.Id;
            flight.FlightStatusLookupId = FlightStatus.Occupied;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus($"Driver #{driver.Id} was set to flight " +
                                       $"#{flight.Id} with car #{autoId} successfully", true);
        }

        public OperationStatus<IEnumerable> GetFlightStatuses()
        {
            var statuses = new EnumParser<FlightStatus>().Parse();

            return new OperationStatus<IEnumerable>("", statuses, true);
        }

        public async Task<OperationStatus> DeleteDriverAndAuto(int? flightId)
        {
            if(flightId == null)
                throw new ArgumentNullException(nameof(flightId));

            var flight = await _database.FlightRepository.FindAsync(flightId);

            if(flight == null)
                return new OperationStatus("Flight doesn't exist", HttpStatusCode.NotFound, false);

            if(flight.FlightStatusLookupId != FlightStatus.Occupied)
                return new OperationStatus(
                    $"Driver and auto cannot be deleted because flight is {flight.Status.Name}",
                    HttpStatusCode.BadRequest,
                    false);

            flight.DriverId = null;
            flight.AutoId = null;
            flight.FlightStatusLookupId = FlightStatus.Free;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus($"Driver and auto was successfully deleted from flight #{flightId}", true);
        }

        public async Task<IEnumerable<FlightDto>> GetFlightsAsync(FlightStatus? status)
        {
            var flights = (await _database.FlightRepository.GetAllAsync()).ToList();

            if (status == null)
            {
                return flights.ToDto();
            }

            return flights.Where(f => f.Status.Id == status).ToDto();
        }

        public async Task<OperationStatus<FlightDto>> GetFlightAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var flight = await _database.FlightRepository.FindAsync(id);

            if (flight == null)
                return new OperationStatus<FlightDto>("Flight doesn't exist", HttpStatusCode.NotFound, false);

            return new OperationStatus<FlightDto>("", flight.ToDto(), true);
        }

        public async Task<OperationStatus> RemoveFlightAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var flight = await _database.FlightRepository.FindAsync(id);

            if (flight == null)
                return new OperationStatus("Flight does not exist", HttpStatusCode.NotFound, false);

            flight.FlightStatusLookupId = FlightStatus.Deleted;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus("Flight was removed", true);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
