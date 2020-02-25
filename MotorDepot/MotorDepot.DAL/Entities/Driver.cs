using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class Driver : AppUser
    {
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
