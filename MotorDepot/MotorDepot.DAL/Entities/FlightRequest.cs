using System;
using MotorDepot.DAL.Entities.Lookup;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequest
    {
        public int Id { get; set; }
        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }
        public string DispatcherId { get; set; }
        public virtual AppUser Dispatcher { get; set; }
        public FlightRequestStatus FlightRequestStatusLookupId { get; set; }
        public virtual FlightRequestStatusLookup Status { get; set; }
        public int? FlightId { get; set; }
        public virtual Flight RequestedFlight { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int EnginePower { get; set; }
        public int EngineCapacity { get; set; }
        public double BootVolume { get; set; }
        public AutoType AutoTypeId { get; set; }
        public virtual AutoTypeLookup AutoType { get; set; }
    }
}
