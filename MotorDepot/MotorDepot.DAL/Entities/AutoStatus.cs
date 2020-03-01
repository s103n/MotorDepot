using MotorDepot.DAL.Entities.Enums;
using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;

namespace MotorDepot.DAL.Entities
{
    public class AutoStatus : BaseEnumEntity<AutoStatusEnum>, IColorEntity
    {
        public ICollection<Auto> Autos { get; set; }
        public string Color { get; set; }
    }
}
