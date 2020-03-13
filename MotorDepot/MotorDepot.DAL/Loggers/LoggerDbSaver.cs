using MotorDepot.DAL.Interfaces;
using MotorDepot.DAL.Interfaces.DbLogger;

namespace MotorDepot.DAL.Loggers
{
    public class LoggerDbSaver : ILoggerDbSaver
    {
        private readonly IApplicationContext _context;
        public LoggerDbSaver(IApplicationContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.Save();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
