using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequestStatus : Status
    {
        public virtual ICollection<FlightRequest> Requests { get; set; }
    }
}
