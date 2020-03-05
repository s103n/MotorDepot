using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities.Lookup
{
    public class FlightStatusLookup : BaseEnumEntity<FlightStatus>
    {
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
