using System;
using MotorDepot.Shared.Enums;

namespace MotorDepot.WEB.Models.FlightRequest
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