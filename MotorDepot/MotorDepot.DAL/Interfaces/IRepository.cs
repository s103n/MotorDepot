using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);
        Task FindAsync(int? id);
        IEnumerable<T> GetAll();
    }
}
