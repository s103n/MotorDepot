using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Identity;
using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Repositories;
using System;
using System.Threading.Tasks;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Interfaces.DbLogger;
using MotorDepot.DAL.Loggers;

namespace MotorDepot.DAL.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<Auto> AutoRepository => new AutoRepository(_context);
        public IRepository<Flight> FlightRepository => new FlightRepository(_context);
        public IRepository<AutoBrand> AutoBrandRepository => new AutoBrandRepository(_context);
        public IRepository<FlightRequest> FlightRequestRepository => new FlightRequestRepository(_context);
        public UserManager UserManager => new UserManager(new UserStore<AppUser>(_context));
        public RoleManager RoleManager => new RoleManager(new RoleStore<IdentityRole>(_context));

        public ILoggerDb<LogEvent> Logger =>
            new DbLogger<LogEvent>(
                new LoggerDbSaver(_context), 
                new LoggerDbLog(_context), 
                new LoggerDbReader(_context));

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region DisposedPattern
        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    AutoRepository.Dispose();
                    FlightRepository.Dispose();
                    AutoBrandRepository.Dispose();
                    FlightRequestRepository.Dispose();
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    Logger.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
