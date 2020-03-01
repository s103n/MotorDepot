using MotorDepot.DAL.Entities.Enums;
using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;

namespace MotorDepot.DAL.Entities
{
    public class AutoType : BaseEnumEntity<AutoTypeEnum>
    {
        public virtual ICollection<Auto> Autos { get; }
    }
}
