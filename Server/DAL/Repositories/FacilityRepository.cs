using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.Enums.Airport;
using DAL.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly AirportDbContext _dbContext;
        private readonly EntityMapper _mapper = new EntityMapper();

        public FacilityRepository(AirportDbContext dbContext) => _dbContext = dbContext;

        public async Task<bool> AnyFlightsInDb() => await _dbContext.Flights.AnyAsync();

        public async Task<Queue<Flight>> GetQueue(Facility facility)
        {
            if (facility == Facility.Runway || facility == Facility.Terminal)
                return new Queue<Flight>();

            var facilityEntity = await _dbContext.Facilities
                .Include(f => f.Flights)
                .FirstOrDefaultAsync(f => f.FacilityId == (int)facility);

            if (facilityEntity!.Flights == null)
                return new Queue<Flight>();

            var flights = facilityEntity.Flights.Select(_mapper.EntityToModel);
            return new Queue<Flight>(flights);
        }

        public async Task<Flight?> GetRunway(Facility facility)
        {
            if (facility != Facility.Runway)
                return null;

            var flightEntity = await _dbContext.Flights
                    .Where(f => f.FacilityId == (int)facility)
                    .FirstOrDefaultAsync();

            if (flightEntity == null)
                return null;

            return _mapper.EntityToModel(flightEntity);
        }

        public async Task<List<Flight>> GetTerminal(Facility facility)
        {
            if (facility != Facility.Terminal)
                return new List<Flight>();

            var flightEntity = await _dbContext.Flights
                    .Where(f => f.FacilityId == (int)facility)
                    .ToListAsync();

            return _mapper.ListEntitiesToModels(flightEntity);
        }
    }
}
