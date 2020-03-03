using MotorDepot.DAL.Entities.Enums;
using System;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequest
    {
        public int Id { get; set; }
        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }
        public string DispatcherId { get; set; }
        public virtual AppUser Dispatcher { get; set; }
        public FlightRequestStatusEnum FlightRequestStatusId { get; set; }
        public virtual FlightRequestStatus Status { get; set; }
        public int? FlightId { get; set; }
        public virtual Flight RequestedFlight { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int EnginePower { get; set; }
        public int EngineCapacity { get; set; }
        public double BootVolume { get; set; }
        public AutoTypeEnum AutoTypeId { get; set; }
        public virtual AutoType AutoType { get; set; }
    }
}
