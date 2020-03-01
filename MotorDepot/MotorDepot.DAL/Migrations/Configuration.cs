using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Enums;

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
            ContextKey = "MotorDepot.DAL.Context.ApplicationContext";
        }

        protected override void Seed(MotorDepot.DAL.Context.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.AutoStatuses.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(AutoStatusEnum))
            //        .OfType<AutoStatusEnum>()
            //        .Select(x => new AutoStatus() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.AutoTypes.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(AutoTypeEnum))
            //        .OfType<AutoTypeEnum>()
            //        .Select(x => new AutoType() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.FlightStatuses.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(FlightStatusEnum))
            //        .OfType<FlightStatusEnum>()
            //        .Select(x => new FlightStatus() { Id = x, Name = x.ToString() })
            //        .ToArray());

            //context.FlightRequestStatuses.AddOrUpdate(
            //    x => x.Id,
            //    Enum.GetValues(typeof(FlightRequestStatusEnum))
            //        .OfType<FlightRequestStatusEnum>()
            //        .Select(x => new FlightRequestStatus() { Id = x, Name = x.ToString() })
            //        .ToArray());
        }
    }
}
