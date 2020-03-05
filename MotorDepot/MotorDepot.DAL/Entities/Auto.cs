using System.Collections.Generic;
using MotorDepot.DAL.Entities.Lookup;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities
{
    public class Auto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Numbers { get; set; }
        public int EnginePower { get; set; } // in horsepower
        public double EngineCapacity { get; set; } // in centimeters in a cube
        public double BootVolumeMax { get; set; } // in liters
        public int AutoBrandId { get; set; }
        public virtual AutoBrand Brand { get; set; }
        public AutoType AutoTypeLookupId { get; set; }
        public virtual AutoTypeLookup Type { get; set; }
        public AutoStatus AutoStatusLookupId { get; set; }
        public virtual AutoStatusLookup Status { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}