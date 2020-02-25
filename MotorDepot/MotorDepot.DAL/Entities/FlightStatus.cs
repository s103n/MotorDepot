using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class FlightStatus : Status
    {
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
