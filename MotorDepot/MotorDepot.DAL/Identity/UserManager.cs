using Microsoft.AspNet.Identity;
using MotorDepot.DAL.Entities;

namespace MotorDepot.DAL.Identity
{
    public class UserManager : UserManager<AppUser>
    {
        public UserManager(IUserStore<AppUser> store) : base(store)
        {
        }
    }
}
