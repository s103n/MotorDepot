using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities.Lookup
{
    public class AutoTypeLookup : BaseEnumEntity<AutoType>
    {
        public virtual ICollection<Auto> Autos { get; }
    }
}
