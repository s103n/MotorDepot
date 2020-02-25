using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        [StringLength(1024, ErrorMessage = "too many symbols", MinimumLength = 20)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int StatusId { get; set; }
        public virtual FlightStatus Status { get; set; }

        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public int? AutoId { get; set; }
        public virtual Auto Auto { get; set; }

        [Required]
        public int DispatcherId { get; set; }
        public virtual Dispatcher Dispatcher { get; set; }
    }
}
