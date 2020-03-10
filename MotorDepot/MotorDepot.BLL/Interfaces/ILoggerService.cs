using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Interfaces
{
    public interface ILoggerService
    {
        void Log(LogEventDto logEvent);
        IEnumerable<LogEventDto> GetLogs(LogType logType);
        OperationStatus<LogEventDto> GetLogById(int? id);
    }
}
