using System.Collections.Generic;
using MotorDepot.WEB.Models.Auto;

namespace MotorDepot.WEB.Models.FlightRequest
{
    public class FlightRequestAcceptViewModel
    {
        public FlightRequestDetailsViewModel Request { get; set; }
        public IEnumerable<AutoSetViewModel> Auto { get; set; }
    }
}