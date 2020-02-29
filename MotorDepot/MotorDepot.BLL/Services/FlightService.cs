using MotorDepot.BLL.BusinessModels;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateAsync(FlightDto flightDto)
        {
            var flight = flightDto.ToFlight();

            await _database.FlightRepository.AddAsync(flight);
        }

        public async Task EditAsync(FlightDto flightDto)
        {
            await _database.FlightRepository.UpdateAsync(flightDto.ToFlight());
        }

        public async Task<FlightDto> GetAsync(object property)
        {
            var refGet = new ReflectionGet<Flight>(property);
            var item = await refGet.GetItem(_database.FlightRepository);

            return item?.ToFlightDto();
        }

        public async Task<OperationStatus> SetStatus(FlightStatus status, int? flightId)
        {
            if (flightId == null)
                return new OperationStatus("", "Unknown flight", false);

            var flight = await _database.FlightRepository.FindAsync(f => f.Id == flightId);

            if (flight == null)
                return new OperationStatus("", "Unknown flight", false);

            flight.StatusId = (int)status;

            await _database.FlightRepository.UpdateAsync(flight);

            return new OperationStatus("", "Status was updated", true);
        }

        public IEnumerable<FlightDto> GetAll(bool deleted = false)
        {
            var flights = _database.FlightRepository.GetAll();

            if (deleted)
            {
                return flights.AsEnumerable().ToFlightDtos();
            }

            return flights.Where(flight => flight.StatusId == (int)FlightStatus.Deleted)
                .AsEnumerable()
                .ToFlightDtos();
        }

        public async Task<FlightDto> GetByIdAsync(int? id)
        {
            if (id == null)
                return null;

            var flight = await _database.FlightRepository.FindAsync(p => p.Id == id);

            return flight.ToFlightDto();
        }

        public async Task RemoveAsync(int? id)
        {
            if (id == null)
                return;

            var flight = await _database.FlightRepository.FindAsync(p => p.Id == id);

            if (flight == null)
                return;

            flight.StatusId = (int)FlightStatus.Deleted;

            await _database.FlightRepository.UpdateAsync(flight);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
