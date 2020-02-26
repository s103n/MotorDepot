using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Context;
using System.Collections.Generic;

namespace MotorDepot.BLL.Services
{
    public class UserService
    {
        public IEnumerable<IdentityRole> GetRoles()
        {
            var x = new ApplicationContext("MotorDepot");
            return x.Roles;
        }
    }
}
