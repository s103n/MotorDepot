using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorDepot.DAL.Context;

namespace MotorDepot.DAL.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new ApplicationContext();
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();
        }
        /////
    }
}
