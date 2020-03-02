using System;
using System.ComponentModel.DataAnnotations;
using MotorDepot.BLL.Infrastructure.Enums;

namespace MotorDepot.WEB.Models
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string DeparturePlace { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ArrivalPlace { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        [Required]
        public FlightStatus Status { get; set; }
        public string AutoName { get; set; }
        public string AutoNumbers { get; set; }
        public string DriverEmail { get; set; }
        public string DriverName { get; set; }
    }
}