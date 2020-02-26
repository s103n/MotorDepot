using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class FlightStatusRepository : IRepository<FlightStatus>
    {

        private readonly ApplicationContext _context;
        public FlightStatusRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FlightStatus item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.FlightStatuses.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FlightStatus item)
        {
            if (item == null)
                return;

            _context.FlightStatuses.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task<FlightStatus> FindAsync(Predicate<FlightStatus> predicate)
        {
            return await _context.FlightStatuses.FirstOrDefaultAsync(fstatus => predicate(fstatus));
        }

        public IQueryable<FlightStatus> GetAll() => _context.FlightStatuses;

        public async Task UpdateAsync(FlightStatus item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
