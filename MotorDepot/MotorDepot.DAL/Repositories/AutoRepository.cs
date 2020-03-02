using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class AutoRepository : IRepository<Auto>
    {
        private readonly ApplicationContext _context;

        public AutoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Auto item)
        {
            _context.Autos.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Auto item)
        {
            _context.Autos.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auto item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task FindAsync(int? id)
        {
            await _context.Autos.FindAsync(id);
        }

        public IEnumerable<Auto> GetAll()
        {
            return _context.Autos;
        }
    }
}
