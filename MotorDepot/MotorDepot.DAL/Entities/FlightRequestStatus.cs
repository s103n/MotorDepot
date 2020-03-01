using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.DAL.Entities.Enums;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequestStatus : BaseEnumEntity<FlightRequestStatusEnum>, IColorEntity
    {
        public virtual ICollection<FlightRequest> Requests { get; set; }
        public string Color { get; set; }
    }
}
