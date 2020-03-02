using System.Collections.Generic;

namespace MotorDepot.DAL.Entities
{
    public class AutoBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
