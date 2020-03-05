using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotorDepot.BLL.BusinessModels;
using MotorDepot.Shared.Enums;

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

            return new OperationStatus("", true);
        }

        public async Task<OperationStatus> EditAutoAsync(AutoDto autoDto)
        {
            if(autoDto == null)
                throw new ArgumentNullException(nameof(autoDto));

            await _database.AutoRepository.UpdateAsync(autoDto.ToEntity());

            return new OperationStatus("", true);
        }

        public async Task<OperationStatus<IEnumerable<AutoDto>>> GetAutosByTypeAsync(AutoType type) // to object property
        {
            var items = (await _database.AutoRepository.GetAllAsync())
                .ToList()
                .Where(x => x.AutoTypeLookupId == type)
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

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
