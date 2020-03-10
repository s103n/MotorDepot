using System;
using MotorDepot.DAL.Entities.Lookup;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities.Logging
{
    public class LogEvent
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        public string Ip { get; set; }
        public string RouteValues { get; set; }
        public string StackTrace { get; set; }
        public virtual LogTypeLookup LogType { get; set; }
        public LogType LogTypeLookupId { get; set; }
    }
}
