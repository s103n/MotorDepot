using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }

        [StringLength(1024, MinimumLength = 20)]
        public string Description { get; set; }

        [Required, StringLength(64, MinimumLength = 3)]
        public string DeparturePlace { get; set; }

        [Required, StringLength(64, MinimumLength = 3)]
        public string ArrivalPlace { get; set; }

        [Required]
        public double Distance { get; set; } // distance from departure place to arrival in meters

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public int StatusId { get; set; }
        public virtual FlightStatus Status { get; set; }

        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }

        public int? AutoId { get; set; }
        public virtual Auto Auto { get; set; }
        
        [Required]
        public string CreatorId { get; set; }
        public virtual AppUser Creator { get; set; }

        public virtual ICollection<FlightRequest> FlightRequests { get; set; }
    }
}
