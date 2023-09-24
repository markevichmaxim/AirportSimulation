using DAL.Entities;
using DAL.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AirportDbContext : DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options) { }

        public virtual DbSet<FlightEntity> Flights { get; set; }
        public virtual DbSet<FacilityEntity> Facilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedFacilities();

            base.OnModelCreating(modelBuilder);
        }
    }
}