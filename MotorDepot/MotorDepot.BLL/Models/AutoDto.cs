using MotorDepot.BLL.Infrastructure.Enums;
using System.Collections.Generic;

namespace MotorDepot.BLL.Models
{
    public class AutoDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Numbers { get; set; }
        public int EnginePower { get; set; }
        public double EngineCapacity { get; set; }
        public double BootVolumeMax { get; set; }
        public AutoBrandDto Brand { get; set; }
        public AutoType Type { get; set; }
        public AutoStatus Status { get; set; }
    }
}
