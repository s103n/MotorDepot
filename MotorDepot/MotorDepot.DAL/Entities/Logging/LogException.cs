using System;

namespace MotorDepot.DAL.Entities.Logging
{
    public class LogException
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
    }
}
