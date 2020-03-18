using MotorDepot.WEB.Models.Flight;
using System.Collections.Generic;

namespace MotorDepot.WEB.Models.User
{
    public class DriverDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<FlightViewModel> Flights { get; set; }
    }
}