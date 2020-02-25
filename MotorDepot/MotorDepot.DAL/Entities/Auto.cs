using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities
{
    public class Auto
    {
        public int Id { get; set; }
        [StringLength(64, MinimumLength = 5)] 
        public string Name { get; set; }

        [Required]
        public int StatusId { get; set; }
        public virtual AutoStatus AutoStatus { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}