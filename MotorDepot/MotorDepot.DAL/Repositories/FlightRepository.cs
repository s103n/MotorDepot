using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private readonly ApplicationContext _context;

        public FlightRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Flight item)
        {
            _context.Flights.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flight item)
        {
            _context.Flights.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task FindAsync(int? id)
        {
            await _context.Flights.FindAsync(id);
        }

        public IEnumerable<Flight> GetAll()
        {
            return _context.Flights;
        }
    }
}
