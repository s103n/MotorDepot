using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.WEB.Models
{
    public class FlightViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string DeparturePlace { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Arrival { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        public int? AutoId { get; set; }
        public string Driver { get; set; }
    }
}