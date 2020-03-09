using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure.Mappers;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Interfaces;

namespace MotorDepot.BLL.Services
{
    public class LogService : ILoggerService
    {
        private readonly IUnitOfWork _database;
        public LogService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public async Task Log(ExceptionDto exception)
        {
            await _database.LogExceptionRepository.AddAsync(exception.ToEntity());
        }
    }
}
