using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class Auto
    {
        public int Id { get; set; }
        [StringLength(64, MinimumLength = 5)] 
        public string Model { get; set; }
        [Required]
        public int AutoBrandId { get; set; }
        public virtual AutoBrand Brand { get; set; }
        [Required]
        public int AutoTypeId { get; set; }
        public virtual AutoType Type { get; set; }
        [Required]
        public int StatusId { get; set; }
        public virtual AutoStatus Status { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}