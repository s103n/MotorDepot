using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorDepot.BLL.Infrastructure.Enums;

namespace MotorDepot.WEB.Models
{
    public class AutoSetViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Numbers { get; set; }
        public string AutoBrand { get; set; }
        public double EngineCapacity { get; set; }
        public double BootVolumeMax { get; set; }
        public string EnginePower { get; set; }
        public AutoType Type { get; set; }
        public AutoStatus Status { get; set; }
    }
}