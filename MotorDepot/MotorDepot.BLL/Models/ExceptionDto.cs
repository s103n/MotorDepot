using System;

namespace MotorDepot.BLL.Models
{
    public class ExceptionDto
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
    }
}
