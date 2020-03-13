using System;

namespace MotorDepot.DAL.Interfaces.DbLogger
{
    public interface ILoggerDbLog<in T> : ILogger<T>, IDisposable where T : class
    {
    }
}
