using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Interfaces.DbLogger;

namespace MotorDepot.DAL.Loggers
{
    public class LoggerDbLog : ILoggerDbLog<LogEvent>
    {
        private readonly IApplicationContext _context;

        public LoggerDbLog(IApplicationContext context)
        {
            _context = context;
        }

        public void Log(LogEvent item)
        {
            _context.LogEvents.Add(item);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
