using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class AutoStatus : Status
    {
        public ICollection<Auto> Autos { get; set; }
    }
}
