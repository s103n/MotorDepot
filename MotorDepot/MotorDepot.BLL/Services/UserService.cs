using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Context;

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
