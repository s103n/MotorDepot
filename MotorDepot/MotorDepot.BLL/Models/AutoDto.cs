using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using MotorDepot.BLL.Infrastructure.Enums;

namespace MotorDepot.BLL.Models
{
    public class AutoDto
    {
        public string Model { get; set; }
        public string Numbers { get; set; }
        public int EnginePower { get; set; }
        public double EngineCapacity { get; set; }
        public double BootVolumeMax { get; set; }
        public string AutoBrand { get; set; }
        public AutoType Type { get; set; }
        public AutoStatus Status { get; set; }
        public ICollection<FlightDto> Flights { get; set; }
    }
}
