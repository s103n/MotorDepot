using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using System.Collections.Generic;

namespace MotorDepot.WEB.Models.User
{
    public class DispatcherDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<FlightViewModel> Flights { get; set; }
        public IEnumerable<FlightRequestDisplayViewModel> FlightRequests { get; set; }
    }
}