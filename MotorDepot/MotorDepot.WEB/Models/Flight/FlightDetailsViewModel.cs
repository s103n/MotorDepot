using System;

namespace MotorDepot.WEB.Models.Flight
{
    public class FlightDetailsViewModel
    {
        public int Id { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int? AutoId { get; set; }
        public string DriverEmail { get; set; }
        public string DispatcherCreatorEmail { get; set; }
    }
}