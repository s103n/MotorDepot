using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class FlightRequestRepository : IRepository<FlightRequest>
    {
        private readonly ApplicationContext _context;

        public FlightRequestRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task AddAsync(FlightRequest item)
        {
            _context.FlightRequests.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FlightRequest item)
        {
            _context.FlightRequests.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FlightRequest item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<FlightRequest> FindAsync(int? id)
        {
            return await _context.FlightRequests.FindAsync(id);
        }

        public async Task<IEnumerable<FlightRequest>> GetAllAsync()
        {
            return await _context.FlightRequests.ToListAsync();
        }
    }
}
