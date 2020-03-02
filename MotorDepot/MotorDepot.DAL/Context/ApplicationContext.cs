using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using MotorDepot.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using MotorDepot.DAL.Infrastructure;

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
        public DbSet<FlightStatus> FlightStatuses { get; set; }
        public DbSet<AutoStatus> AutoStatuses { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<AutoType> AutoTypes { get; set; }
        public DbSet<AutoBrand> AutoBrands { get; set; }
        public DbSet<FlightRequest> FlightRequests { get; set; }
        public DbSet<FlightRequestStatus> FlightRequestStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightRequestStatus>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<AutoType>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<AutoStatus>()
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<FlightStatus>()
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
