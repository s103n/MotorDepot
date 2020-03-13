using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;
using System.Collections;
using System.Collections.Generic;

namespace MotorDepot.BLL.Interfaces
{
    public interface ILoggerService
    {
        void Log(LogEventDto logEvent);
        IEnumerable<LogEventDto> GetLogs(LogType logType);
        OperationStatus<LogEventDto> GetLogById(int? id);
        IEnumerable GetLogTypes();
    }
}
