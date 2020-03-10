using System;

namespace MotorDepot.WEB.Models.Logging
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime Time { get; set; }
        public string LogType { get; set; }
    }
}