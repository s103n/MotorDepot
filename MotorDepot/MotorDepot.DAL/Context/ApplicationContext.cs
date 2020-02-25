using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;
using System.Data.Entity;

namespace MotorDepot.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString)
        {
        }

        static ApplicationContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightStatus> FlightStatuses { get; set; }
        public DbSet<AutoStatus> AutoStatuses { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Dispatcher> Dispatchers { get; set; }
    }
}
