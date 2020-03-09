using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Repositories
{
    public class LogExceptionRepository : IRepository<LogException>
    {
        private readonly ApplicationContext _context;

        public LogExceptionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }

        public async Task AddAsync(LogException item)
        {
            _context.LogExceptions.Add(item);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LogException item)
        {
            _context.LogExceptions.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LogException item)
        {
            _context.LogExceptions.AddOrUpdate(item);

            await _context.SaveChangesAsync();
        }

        public async Task<LogException> FindAsync(int? id)
        {
            return await _context.LogExceptions.FindAsync(id);
        }

        public async Task<IEnumerable<LogException>> GetAllAsync()
        {
            return await _context.LogExceptions.ToListAsync();
        }
    }
}
