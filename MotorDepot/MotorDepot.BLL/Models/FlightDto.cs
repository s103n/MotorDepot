using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Migrations;

namespace MotorDepot.BLL.Models
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string Description { get; set; }
        public FlightStatusDto Status { get; set; }
        public DateTime CreateDate { get; set; }
        public AutoDto Auto { get; set; }
        public UserDto Driver { get; set; }
    }
}
