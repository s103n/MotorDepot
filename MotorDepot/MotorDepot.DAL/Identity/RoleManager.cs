using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MotorDepot.DAL.Identity
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}
