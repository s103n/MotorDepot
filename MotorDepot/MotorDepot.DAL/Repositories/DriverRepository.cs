using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;

namespace MotorDepot.DAL.Repositories
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly ApplicationContext _context;

        public DriverRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Driver item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Drivers.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Driver item)
        {
            if (item == null)
                return;

            _context.Drivers.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task<Driver> FindAsync(Predicate<Driver> predicate)
        {
            return await _context.Drivers.FirstOrDefaultAsync(driver => predicate(driver));
        }

        public IQueryable<Driver> GetAll()
        {
            return _context.Drivers;
        }

        public async Task UpdateAsync(Driver item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
