using System;

namespace MotorDepot.DAL.Interfaces.DbLogger
{
    public interface ILoggerDbSaver : IDisposable
    {
        void Save();
    }
}
