using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.DAL.Entities.Enums;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequestStatus : BaseEnumEntity<FlightRequestStatusEnum>
    {
        public virtual ICollection<FlightRequest> Requests { get; set; }
    }
}
