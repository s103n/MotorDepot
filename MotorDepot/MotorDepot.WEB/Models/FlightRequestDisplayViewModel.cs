using MotorDepot.BLL.Infrastructure.Enums;
using System;

namespace MotorDepot.WEB.Models
{
    public class FlightRequestDisplayViewModel
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public FlightRequestStatus Status { get; set; }
        public int RequestedFlightId { get; set; }
        public DateTime Date { get; set; }
    }
}