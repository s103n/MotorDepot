using MotorDepot.BLL.BusinessModels;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using MotorDepot.Shared.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Services
{
    public class AutoService : IAutoService
    {
        private readonly IUnitOfWork _database;

        public AutoService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task<OperationStatus> CreateAutoAsync(AutoDto autoDto)
        {
            if (autoDto == null)
                throw new ArgumentNullException(nameof(autoDto));

            autoDto.Status = AutoStatus.Usable;

            await _database.AutoRepository.AddAsync(autoDto.ToEntity());

            return new OperationStatus($"Auto {autoDto.Model} {autoDto.Brand.Name} was created", true);
        }

        public async Task<OperationStatus> EditAutoAsync(AutoDto autoDto)
        {
            if (autoDto == null)
                throw new ArgumentNullException(nameof(autoDto));

            await _database.AutoRepository.UpdateAsync(autoDto.ToEntity());

            return new OperationStatus($"Auto #{autoDto.Id} was updated", true);
        }

        public async Task<OperationStatus<IEnumerable<AutoDto>>> GetAutosByTypeAsync(AutoType type)
        {
            var items = (await _database.AutoRepository.GetAllAsync())
                .ToList()
                .Where(x => x.AutoTypeLookupId == type && x.AutoStatusLookupId == AutoStatus.Usable)
                .ToDto();

            return new OperationStatus<IEnumerable<AutoDto>>("", items, true);
        }

        public async Task<OperationStatus<IEnumerable<AutoBrandDto>>> GetBrandsAsync()
        {
            var brands = await _database.AutoBrandRepository.GetAllAsync();

            return new OperationStatus<IEnumerable<AutoBrandDto>>("Ok", brands.ToDto(), true);
        }

        public OperationStatus<IEnumerable> GetAutoTypes()
        {
            var parser = new EnumParser<AutoType>().Parse();

            return new OperationStatus<IEnumerable>("", parser, true);
        }

        public async Task<OperationStatus<IEnumerable<AutoDto>>> GetAutosAsync()
        {
            var autos = await _database.AutoRepository.GetAllAsync();

            return new OperationStatus<IEnumerable<AutoDto>>("", autos.ToDto(), true);
        }

        public async Task<bool> IsInFlightAsync(int autoId)
        {
            var auto = await _database.AutoRepository.FindAsync(autoId);

            return auto.Flights.Any(flight => flight.Status.Id == FlightStatus.Performed
                                              || flight.Status.Id == FlightStatus.Occupied);
        }

        public async Task<OperationStatus<AutoDto>> GetAutoById(int? autoId)
        {
            if(autoId == null)
                throw new ArgumentNullException(nameof(autoId));

            var auto = await _database.AutoRepository.FindAsync(autoId);

            if(auto == null)
                return new OperationStatus<AutoDto>("Auto doesn't exist", HttpStatusCode.NotFound, false);

            return new OperationStatus<AutoDto>("", auto.ToDto(), true);
        }

        public async Task<OperationStatus> SetStatus(AutoStatus status, int autoId)
        {
            var auto = await _database.AutoRepository.FindAsync(autoId);

            if(auto == null)
                return new OperationStatus("Auto doesn't exist", HttpStatusCode.NotFound, false);

            auto.AutoStatusLookupId = status;

            await _database.AutoRepository.UpdateAsync(auto);

            return new OperationStatus("Auto was updated", true);
        }

        public OperationStatus<IEnumerable> GetAutoStatuses()
        {
            var parser = new EnumParser<AutoStatus>().Parse();

            return new OperationStatus<IEnumerable>("", parser, true);
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
