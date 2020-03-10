using System;

namespace MotorDepot.WEB.Models.Logging
{
    public class LogExceptionDetailsViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
    }
}