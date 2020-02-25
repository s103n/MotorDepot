using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;

namespace MotorDepot.DAL.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<AppContext>
    {
        public override void InitializeDatabase(AppContext context)
        {
            context.Roles.Add(new IdentityRole("admin"));
            context.Roles.Add(new IdentityRole("dispatcher"));
            context.Roles.Add(new IdentityRole("driver"));
            context.Roles.Add(new IdentityRole("root"));

            base.InitializeDatabase(context);
        }
    }
}
