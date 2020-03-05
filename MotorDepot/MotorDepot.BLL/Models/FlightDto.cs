using MotorDepot.Shared.Enums;
using DateTime = System.DateTime;

namespace MotorDepot.BLL.Models
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public FlightStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public AutoDto Auto { get; set; }
        public DriverDto Driver { get; set; }
        public DispatcherDto DispatcherCreator { get; set; }
    }
}
