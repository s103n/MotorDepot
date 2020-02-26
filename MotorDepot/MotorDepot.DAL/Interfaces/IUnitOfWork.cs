using MotorDepot.DAL.Entities;
using System;
using System.Threading.Tasks;
using MotorDepot.DAL.Identity;

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
        IRepository<AutoType> AutoTypeRepository { get; }
        IRepository<AutoBrand> AutoBrandRepository { get; }
        UserManager UserManager { get; }
        RoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
