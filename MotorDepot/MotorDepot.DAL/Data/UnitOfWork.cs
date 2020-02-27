using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Identity;

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
        public IRepository<AutoType> AutoTypeRepository => new AutoTypeRepository(_context);
        public IRepository<AutoBrand> AutoBrandRepository => new AutoBrandRepository(_context);
        public UserManager UserManager => new UserManager(new UserStore<AppUser>(_context));
        public RoleManager RoleManager => new RoleManager(new RoleStore<IdentityRole>(_context));

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
