﻿using MotorDepot.Shared.Enums;

namespace MotorDepot.WEB.Models.Auto
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