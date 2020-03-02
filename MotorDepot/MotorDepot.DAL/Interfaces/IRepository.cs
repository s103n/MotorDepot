using MotorDepot.DAL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<ValidationErrors> AddAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
        Task<T> FindAsync(int? id);
        IEnumerable<T> GetAll();
    }
}
