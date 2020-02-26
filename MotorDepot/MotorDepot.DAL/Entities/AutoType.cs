using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class AutoType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string Name { get; set; }
        public virtual ICollection<Auto> Autos { get; }
    }
}
