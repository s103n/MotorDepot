using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class Dispatcher : AppUser
    {
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
