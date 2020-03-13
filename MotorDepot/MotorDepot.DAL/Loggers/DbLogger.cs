using MotorDepot.DAL.Interfaces.DbLogger;
using System;
using System.Collections.Generic;

namespace MotorDepot.DAL.Loggers
{
    public class DbLogger<T> : ILoggerDb<T>, IDisposable where T : class
    {
        public ILoggerDbLog<T> LogDbLogger { get; set; }
        public ILoggerDbSaver LogDbSaver { get; set; }
        public ILoggerDbReader<T> LogDbReader { get; set; }

        public DbLogger(ILoggerDbSaver logLogDbSaver,
            ILoggerDbLog<T> logger,
            ILoggerDbReader<T> logDbReader)
        {
            LogDbLogger = logger;
            LogDbReader = logDbReader;
            LogDbSaver = logLogDbSaver;
        }

        public void Log(T item)
        {
            LogDbLogger.Log(item);
            LogDbSaver.Save();
        }

        public IEnumerable<T> GetLogs()
        {
            return LogDbReader.GetLogs();
        }

        public T GetLog(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return LogDbReader.Find(id);
        }

        public void Dispose()
        {
            LogDbLogger?.Dispose();
            LogDbSaver?.Dispose();
            LogDbReader?.Dispose();
        }
    }
}
