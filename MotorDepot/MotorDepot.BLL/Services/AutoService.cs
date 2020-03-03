using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;
using System;
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

        public async Task<OperationStatus<AutoDto>> CreateAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public async Task EditAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
