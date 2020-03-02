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
    public class AutoBrandRepository : IRepository<AutoBrand>
    {
        private readonly ApplicationContext _context;

        public AutoBrandRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ValidationErrors> AddAsync(AutoBrand item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            _context.AutoBrands.Add(item);

            return await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrand item)
        {
            if (item == null)
                return;

            _context.AutoBrands.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrand item)
        {
            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<AutoBrand> FindAsync(int? id)
        {
            return await _context.AutoBrands.FindAsync(id);
        }

        public IEnumerable<AutoBrand> GetAll() => _context.AutoBrands;
    }
}
