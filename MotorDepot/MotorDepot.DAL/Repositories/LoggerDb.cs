using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MotorDepot.DAL.Context;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Interfaces;

namespace MotorDepot.DAL.Repositories
{
    public class LoggerDb : ILogger<LogEvent>
    {
        private readonly ApplicationContext _context;
        public LoggerDb(ApplicationContext context)
        {
            _context = context;
        }
        public void Log(LogEvent item)
        {
            _context.LogEvents.Add(item);
            _context.SaveChanges();
        }

        public LogEvent Find(int? id)
        {
            return _context.LogEvents.Find(id);
        }

        public IEnumerable<LogEvent> GetLogs()
        {
            return _context.LogEvents;
        }
    }

    public class LoggerDbAsync : ILoggerAsync<LogEvent>
    {
        private readonly ApplicationContext _context;
        public LoggerDbAsync(ApplicationContext context)
        {
            _context = context;
        }
        public async Task LogAsync(LogEvent item)
        {
            _context.LogEvents.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<LogEvent> GetAsync(int? id)
        {
            return await _context.LogEvents.FindAsync(id);
        }

        public async Task<IEnumerable<LogEvent>> GetLogsAsync()
        {
            return await _context.LogEvents.ToListAsync();
        }
    }
}
