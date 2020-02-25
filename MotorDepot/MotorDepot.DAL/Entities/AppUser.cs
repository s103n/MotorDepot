using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MotorDepot.DAL.Entities
{
    public abstract class AppUser : IdentityUser
    {
        [Required]
        [StringLength(24, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 2)]
        public string LastName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime LastSignUp { get; set; }
    }
}
