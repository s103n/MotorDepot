using System;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Models
{
    public class LogEventDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        public string Ip { get; set; }
        public string RouteValues { get; set; }
        public string StackTrace { get; set; }
        public LogType LogType { get; set; }
    }
}
