using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MotorDepot.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime? LastSignUp { get; set; }
    }
}
