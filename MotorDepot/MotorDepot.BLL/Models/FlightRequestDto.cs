using System;
using MotorDepot.BLL.Infrastructure.Enums;
using FlightRequestStatus = MotorDepot.BLL.Infrastructure.Enums.FlightRequestStatus;

namespace MotorDepot.BLL.Models
{
    public class FlightRequestDto
    {
        public int Id { get; set; }

        public UserDto Driver { get; set; }

        public UserDto Dispatcher { get; set; }

        public FlightRequestStatus Status { get; set; }

        public FlightDto RequestedFlight { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int EnginePower { get; set; }

        public int EngineCapacity { get; set; }

        public double BootVolume { get; set; }

        public AutoType AutoType { get; set; }
    }
}
