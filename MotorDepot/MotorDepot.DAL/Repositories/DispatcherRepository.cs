using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class DispatcherRepository : IRepository<Dispatcher>
    {
        private readonly ApplicationContext _context;
        public DispatcherRepository(ApplicationContext context)
        {
            _context = context; ;
        }

        public async Task AddAsync(Dispatcher item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Dispatchers.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Dispatcher item)
        {
            if (item == null)
                return;

            _context.Dispatchers.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task<Dispatcher> FindAsync(Predicate<Dispatcher> predicate)
        {
            return await _context.Dispatchers.FirstOrDefaultAsync(dispatcher => predicate(dispatcher));
        }

        public IQueryable<Dispatcher> GetAll()
        {
            return _context.Dispatchers;
        }

        public async Task UpdateAsync(Dispatcher item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
