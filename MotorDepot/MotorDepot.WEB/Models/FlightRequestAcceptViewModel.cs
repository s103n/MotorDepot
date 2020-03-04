using System.Collections.Generic;

namespace MotorDepot.WEB.Models
{
    public class FlightRequestAcceptViewModel
    {
        public FlightRequestDetailsViewModel Request { get; set; }
        public IEnumerable<AutoSetViewModel> Auto { get; set; }
    }
}