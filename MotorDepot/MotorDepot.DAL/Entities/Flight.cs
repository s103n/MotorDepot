using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MotorDepot.DAL.Entities.Enums;

namespace MotorDepot.DAL.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public double Distance { get; set; } // distance from departure place to arrival in meters
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public FlightStatusEnum StatusId { get; set; }
        public virtual FlightStatus Status { get; set; }
        public string DriverId { get; set; }
        public virtual AppUser Driver { get; set; }
        public int? AutoId { get; set; }
        public virtual Auto Auto { get; set; }
        public string CreatorId { get; set; }
        public virtual AppUser Creator { get; set; }
        public virtual ICollection<FlightRequest> FlightRequests { get; set; }
    }
}
