using MotorDepot.Shared.Enums;
using System;

namespace MotorDepot.WEB.Models.Flight
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public DateTime CreateDate { get; set; }
        public FlightStatus Status { get; set; }
        public string AutoName { get; set; }
        public string AutoNumbers { get; set; }
        public int AutoId { get; set; }
        public string DriverEmail { get; set; }
        public string DriverName { get; set; }
        public int Distance { get; set; }
    }
}