using MotorDepot.DAL.Migrations;

namespace MotorDepot.BLL.Models
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int StatusId { get; set; }
        public int? AutoId { get; set; }
        public string DriverId { get; set; }
    }
}
