using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;

namespace MotorDepot.DAL.Context
{
    public class AppContext : IdentityDbContext<AppUser>
    {
        public AppContext(string connectionString) : base(connectionString)
        {
        }

        static AppContext()
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
