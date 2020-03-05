using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using MotorDepot.DAL.Entities;

namespace MotorDepot.DAL.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        public override void InitializeDatabase(ApplicationContext context)
        {
            context.Roles.Add(new IdentityRole("admin"));
            context.Roles.Add(new IdentityRole("dispatcher"));
            context.Roles.Add(new IdentityRole("driver"));
            context.Roles.Add(new IdentityRole("root"));

            context.AutoBrands.Add(new AutoBrand { Name = "Chevrolet"});
            context.AutoBrands.Add(new AutoBrand { Name = "Volkswagen" });
            context.AutoBrands.Add(new AutoBrand { Name = "Tesla" });
            context.AutoBrands.Add(new AutoBrand { Name = "Toyota" });
            context.AutoBrands.Add(new AutoBrand { Name = "Mercedes-Benz" });

            base.InitializeDatabase(context);
        }
    }
}
