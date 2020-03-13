using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.DAL.Entities.Lookup;

namespace MotorDepot.DAL.Interfaces
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<Flight> Flights { get; set; }
        DbSet<FlightStatusLookup> FlightStatusLookups { get; set; }
        DbSet<AutoStatusLookup> AutoStatusLookups { get; set; }
        DbSet<Auto> Autos { get; set; }
        DbSet<AutoTypeLookup> AutoTypeLookups { get; set; }
        DbSet<AutoBrand> AutoBrands { get; set; }
        DbSet<FlightRequest> FlightRequests { get; set; }
        DbSet<FlightRequestStatusLookup> FlightRequestStatusLookups { get; set; }
        DbSet<LogEvent> LogEvents { get; set; }
        void Save();
        Task SaveChangesAsync();
    }
}
