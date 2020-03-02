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
    public class AutoRepository : IRepository<Auto>
    {
        private readonly ApplicationContext _context;
        public AutoRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ValidationErrors> AddAsync(Auto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (item.Status == null)
                throw new ArgumentNullException(nameof(item.Status));

            _context.Autos.Add(item);

            return await _context.SaveChangesAsync();
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

        public async Task<Auto> FindAsync(int? id)
        {
            return await _context.Autos.FindAsync(id);
        }

        public IEnumerable<Auto> GetAll()
        {
            return _context.Autos;
        }
    }
}
