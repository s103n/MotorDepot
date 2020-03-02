using MotorDepot.DAL.Entities.Enums;
using System.Collections.Generic;

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
        public AutoTypeEnum AutoTypeId { get; set; }
        public virtual AutoType Type { get; set; }
        public AutoStatusEnum StatusId { get; set; }
        public virtual AutoStatus Status { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}