using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class AutoBrandRepository : IRepository<AutoBrand>
    {
        private readonly ApplicationContext _context;

        public AutoBrandRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AutoBrand item)
        {
            _context.AutoBrands.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrand item)
        {
            _context.AutoBrands.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrand item)
        {
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<AutoBrand> FindAsync(int? id)
        {
            return await _context.AutoBrands.FindAsync(id);
        }

        public async Task<IEnumerable<AutoBrand>> GetAllAsync()
        {
            return await _context.AutoBrands.ToListAsync();
        } 
    }
}
