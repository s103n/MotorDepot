using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.DAL.Interfaces
{
    public interface ILogger<T> where T : class
    {
        void Log(T item);
        T Find(int? id);
        IEnumerable<T> GetLogs();
    }

    public interface ILoggerAsync<T> where T : class
    {
        Task LogAsync(T item);
        Task<T> GetAsync(int? id);
        Task<IEnumerable<T>> GetLogsAsync();
    }
}
