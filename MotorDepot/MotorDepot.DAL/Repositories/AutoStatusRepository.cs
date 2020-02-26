using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class AutoStatusRepository : IRepository<AutoStatus>
    {
        private readonly ApplicationContext _context;
        public AutoStatusRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AutoStatus item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.AutoStatuses.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoStatus item)
        {
            if (item == null)
                return;

            _context.AutoStatuses.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoStatus item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<AutoStatus> FindAsync(Predicate<AutoStatus> predicate)
        {
            return await _context.AutoStatuses.FirstOrDefaultAsync(astatus => predicate(astatus));
        }

        public IQueryable<AutoStatus> GetAll() => _context.AutoStatuses;
    }
}
