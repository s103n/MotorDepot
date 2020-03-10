using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Lookup;
using MotorDepot.Shared.Enums;

namespace MotorDepot.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MotorDepot.DAL.Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MotorDepot.DAL.Context.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.AutoStatusLookups.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(AutoStatus))
            //        .OfType<AutoStatus>()
            //        .Select(x => new AutoStatusLookup() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.AutoTypeLookups.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(AutoType))
            //        .OfType<AutoType>()
            //        .Select(x => new AutoTypeLookup() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.FlightStatusLookups.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(FlightStatus))
            //        .OfType<FlightStatus>()
            //        .Select(x => new FlightStatusLookup() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.FlightRequestStatusLookups.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(FlightRequestStatus))
            //        .OfType<FlightRequestStatus>()
            //        .Select(x => new FlightRequestStatusLookup() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.Roles.AddOrUpdate(new IdentityRole("admin"));
            //context.Roles.AddOrUpdate(new IdentityRole("dispatcher"));
            //context.Roles.AddOrUpdate(new IdentityRole("driver"));
            //context.Roles.AddOrUpdate(new IdentityRole("root"));

            //context.AutoBrands.AddOrUpdate(new AutoBrand { Name = "Chevrolet" });
            //context.AutoBrands.AddOrUpdate(new AutoBrand { Name = "Volkswagen" });
            //context.AutoBrands.AddOrUpdate(new AutoBrand { Name = "Tesla" });
            //context.AutoBrands.AddOrUpdate(new AutoBrand { Name = "Toyota" });
            //context.AutoBrands.AddOrUpdate(new AutoBrand { Name = "Mercedes-Benz" });
        }
    }
}
