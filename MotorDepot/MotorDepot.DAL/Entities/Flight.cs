using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }

        [StringLength(1024, ErrorMessage = "too many symbols in description", MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string DeparturePlace { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 3)]
        public string ArrivalPlace { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int StatusId { get; set; }
        public virtual FlightStatus Status { get; set; }

        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }

        public int? AutoId { get; set; }
        public virtual Auto Auto { get; set; }
    }
}
