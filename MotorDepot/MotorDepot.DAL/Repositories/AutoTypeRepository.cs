using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class AutoTypeRepository : IRepository<AutoType>
    {
        private readonly ApplicationContext _context;
        public AutoTypeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AutoType item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.AutoTypes.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoType item)
        {
            if (item == null)
                return;

            _context.AutoTypes.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoType item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<AutoType> FindAsync(Predicate<AutoType> predicate)
        {
            return await _context.AutoTypes.FirstOrDefaultAsync(atype => predicate(atype));
        }

        public IQueryable<AutoType> GetAll() => _context.AutoTypes;
    }
}
