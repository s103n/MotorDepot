using System;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Models
{
    public class FlightRequestDto
    {
        public int Id { get; set; }

        public DriverDto Driver { get; set; }

        public DispatcherDto Dispatcher { get; set; }

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
