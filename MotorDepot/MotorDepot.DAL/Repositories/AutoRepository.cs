using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
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
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (item.Status == null)
                throw new ArgumentNullException(nameof(item.Status));

            _context.Autos.Add(item);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Auto item)
        {
            var auto = await _context.Autos.FindAsync(item);

            if (auto == null)
                return;

            _context.Autos.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auto item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<Auto> FindAsync(Predicate<Auto> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(auto => predicate(auto));
        }

        public IQueryable<Auto> GetAll()
        {
            return _context.Autos;
        }
    }
}
