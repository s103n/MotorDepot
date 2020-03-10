using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Lookup;
using MotorDepot.DAL.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using MotorDepot.DAL.Entities.Logging;

namespace MotorDepot.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext() : base("MotorDepot")
        {
        }

        public ApplicationContext(string connectionString) : base(connectionString)
        {
        }

        static ApplicationContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightStatusLookup> FlightStatusLookups { get; set; }
        public DbSet<AutoStatusLookup> AutoStatusLookups { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<AutoTypeLookup> AutoTypeLookups { get; set; }
        public DbSet<AutoBrand> AutoBrands { get; set; }
        public DbSet<FlightRequest> FlightRequests { get; set; }
        public DbSet<FlightRequestStatusLookup> FlightRequestStatusLookups { get; set; }
        public DbSet<LogEvent> LogEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightRequestStatusLookup>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<AutoTypeLookup>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<AutoStatusLookup>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<FlightStatusLookup>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }

        public new async Task<ValidationErrors> SaveChangesAsync()
        {
            try
            {
                await base.SaveChangesAsync();

                return new ValidationErrors();
            }
            catch (DbEntityValidationException ex)
            {
                return new ValidationErrors(ex.EntityValidationErrors);
            }
        }
    }
}
