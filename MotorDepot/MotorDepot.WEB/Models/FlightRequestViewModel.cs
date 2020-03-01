using MotorDepot.BLL.Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.WEB.Models
{
    public class FlightRequestViewModel
    {
        [Required]
        public string DriverId { get; set; }

        public string DispatcherId { get; set; }

        [Required]
        public FlightRequestStatus Status { get; set; }

        [Required]
        public int RequestedFlightId { get; set; }

        [StringLength(256), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int EnginePower { get; set; }

        [Required]
        public int EngineCapacity { get; set; }

        [Required]
        public double BootVolume { get; set; }

        [Required]
        public AutoType AutoType { get; set; }
    }
}