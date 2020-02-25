using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
        Task<T> FindAsync(Predicate<T> predicate);
        IQueryable<T> GetAll();
    }
}
