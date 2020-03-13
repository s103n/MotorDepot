using System;
using System.Collections.Generic;

namespace MotorDepot.DAL.Interfaces.DbLogger
{
    public interface ILoggerDbReader<out T> : IDisposable where T : class
    {
        IEnumerable<T> GetLogs();

        T Find(int? id);
    }
}
