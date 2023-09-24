using Core.Models.Enums.Airport;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Utilities
{
    /// <summary>
    /// Utility class for seeding facility data into the database.
    /// </summary>
    public static class SeedData
    {
        public static ModelBuilder SeedFacilities(this ModelBuilder modelBuilder)
        {
            var facilities = Enum.GetValues<Facility>()
            .Select(facility => new FacilityEntity
            {
                FacilityId = (int)facility,
                FacilityName = $"{facility}"
            })
            .ToList();

            modelBuilder.Entity<FacilityEntity>().HasData(facilities);

            return modelBuilder;
        }
    }
}
