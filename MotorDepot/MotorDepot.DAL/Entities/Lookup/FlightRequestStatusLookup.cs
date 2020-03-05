using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities.Lookup
{
    public class FlightRequestStatusLookup : BaseEnumEntity<FlightRequestStatus>
    {
        public virtual ICollection<FlightRequest> Requests { get; set; }
    }
}
