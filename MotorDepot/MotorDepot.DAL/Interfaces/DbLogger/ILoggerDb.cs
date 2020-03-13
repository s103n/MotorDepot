using System;
using System.Collections.Generic;

namespace MotorDepot.DAL.Interfaces.DbLogger
{
    public interface ILoggerDb<T> : IDisposable where T : class
    {
        IEnumerable<T> GetLogs();
        void Log(T item);
        T GetLog(int? id);
    }
}
