using System.Collections.Generic;

namespace MotorDepot.BLL.Models
{
    public class AutoBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoDto> Autos { get; set; }
    }
}
