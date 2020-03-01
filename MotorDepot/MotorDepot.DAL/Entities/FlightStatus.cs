using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.DAL.Entities.Enums;

namespace MotorDepot.DAL.Entities
{
    public class FlightStatus : BaseEnumEntity<FlightStatusEnum>, IColorEntity
    {
        public virtual ICollection<Flight> Flights { get; set; }
        public string Color { get; set; }
    }
}
