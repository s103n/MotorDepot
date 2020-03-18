using MotorDepot.WEB.Models.Flight;
using System.Collections.Generic;

namespace MotorDepot.WEB.Models.Auto
{
    public class AutoDetailsViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Numbers { get; set; }
        public int EnginePower { get; set; }
        public double EngineCapacity { get; set; }
        public double BootVolumeMax { get; set; }
        public string AutoBrand { get; set; }
        public string AutoType { get; set; }
        public string AutoStatus { get; set; }
        public IEnumerable<FlightViewModel> Flights { get; set; }
    }
}