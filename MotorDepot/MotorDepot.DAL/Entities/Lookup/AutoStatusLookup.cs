using System.Collections.Generic;
using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Entities.Lookup
{
    public class AutoStatusLookup : BaseEnumEntity<AutoStatus> 
    {
        public ICollection<Auto> Autos { get; set; }
    }
}
