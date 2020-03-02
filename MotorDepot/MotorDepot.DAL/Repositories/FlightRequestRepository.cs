using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Infrastructure;
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

        public async Task<ValidationErrors> AddAsync(FlightRequest item)
        {
            _context.FlightRequests.Add(item);

            return await _context.SaveChangesAsync();
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

        public IEnumerable<FlightRequest> GetAll()
        {
            return _context.FlightRequests;
        }
    }
}
