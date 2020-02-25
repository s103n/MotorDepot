using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class AutoStatus : Status
    {
        public ICollection<Auto> Autos { get; set; }
    }
}
