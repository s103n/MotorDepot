using MotorDepot.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AutoStatus> AutoStatusRepository { get; }
        IRepository<Auto> AutoRepository { get; }
        IRepository<FlightStatus> FlightStatusRepository { get; }
        IRepository<Flight> FlightRepository { get; }
        IRepository<Dispatcher> DispatcherRepository { get; }
        IRepository<Driver> DriverRepository { get; }
        Task SaveAsync();
    }
}
