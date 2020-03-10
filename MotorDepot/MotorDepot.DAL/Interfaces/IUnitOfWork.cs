using MotorDepot.DAL.Entities;
using System;
using System.Threading.Tasks;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Identity;
using MotorDepot.DAL.Infrastructure;

namespace MotorDepot.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Auto> AutoRepository { get; }
        IRepository<Flight> FlightRepository { get; }
        IRepository<AutoBrand> AutoBrandRepository { get; }
        IRepository<FlightRequest> FlightRequestRepository { get; }
        UserManager UserManager { get; }
        RoleManager RoleManager { get; }
        ILogger<LogEvent> LoggerDb { get; }
        Task<ValidationErrors> SaveAsync();
    }
}
