using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Interfaces.DbLogger;
using System.Collections.Generic;
using System.Linq;

namespace MotorDepot.DAL.Loggers
{
    public class LoggerDbReader : ILoggerDbReader<LogEvent>
    {
        private readonly IApplicationContext _context;

        public LoggerDbReader(IApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<LogEvent> GetLogs()
        {
            return _context.LogEvents.ToList();
        }

        public LogEvent Find(int? id)
        {
            return _context.LogEvents.Find(id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
