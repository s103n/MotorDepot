using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MotorDepot.DAL.Infrastructure;

namespace MotorDepot.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private readonly ApplicationContext _context;

        public FlightRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ValidationErrors> AddAsync(Flight item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Flights.Add(item);

            return await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flight item)
        {
            if (item == null)
                return;

            _context.Flights.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<Flight> FindAsync(int? id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public IEnumerable<Flight> GetAll()
        {
            return _context.Flights;
        }
    }
}
