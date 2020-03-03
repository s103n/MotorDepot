using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorDepot.BLL.Infrastructure.Enums;

namespace MotorDepot.WEB.Models
{
    public class FlightRequestDetailsViewModel
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public string DriverEmail { get; set; }
        public int RequestedFlightId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int EnginePower { get; set; }
        public int EngineCapacity { get; set; }
        public double BootVolume { get; set; }
        public AutoType AutoType { get; set; }
    }
}