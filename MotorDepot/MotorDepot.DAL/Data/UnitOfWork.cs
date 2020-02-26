using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Repositories;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<AutoStatus> AutoStatusRepository => new AutoStatusRepository(_context);

        public IRepository<Auto> AutoRepository => new AutoRepository(_context);

        public IRepository<FlightStatus> FlightStatusRepository => new FlightStatusRepository(_context);

        public IRepository<Flight> FlightRepository => new FlightRepository(_context);

        public IRepository<Dispatcher> DispatcherRepository => new DispatcherRepository(_context);

        public IRepository<Driver> DriverRepository => new DriverRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
