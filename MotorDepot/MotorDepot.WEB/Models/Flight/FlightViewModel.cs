using MotorDepot.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.WEB.Models.Flight
{
    public class FlightViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Departure")]
        public string DeparturePlace { get; set; }
        [Display(Name = "Arrival")]
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