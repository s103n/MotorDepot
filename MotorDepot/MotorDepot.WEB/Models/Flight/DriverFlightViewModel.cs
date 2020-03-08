using System.Collections.Generic;

namespace MotorDepot.WEB.Models.Flight
{
    public class DriverFlightViewModel
    {
        public FlightViewModel CurrentFlight { get; set; }

        public IEnumerable<FlightViewModel> Flights { get; set; }
    }
}