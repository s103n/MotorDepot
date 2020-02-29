using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class FlightRequest
    {
        public int Id { get; set; }

        [Required]
        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }

        public string DispatcherId { get; set; }
        public virtual AppUser Dispatcher { get; set; }

        [Required]
        public int FlightRequestStatusId { get; set; }
        public virtual FlightRequestStatus Status { get; set; }

        public int? FlightId { get; set; }
        public virtual Flight RequestedFlight { get; set; }

        [StringLength(1024, MinimumLength = 10)]
        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int EnginePower { get; set; }

        [Required]
        public int EngineCapacity { get; set; }

        [Required]
        public double BootVolume { get; set; }

        [Required]
        public int AutoTypeId { get; set; }
        public virtual AutoType AutoType { get; set; }
    }
}
