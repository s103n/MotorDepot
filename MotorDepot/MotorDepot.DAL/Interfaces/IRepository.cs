using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
        Task<T> FindAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
