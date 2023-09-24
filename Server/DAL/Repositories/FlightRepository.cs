using Core.Interfaces.Repositories;
using Core.Models;
using DAL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly EntityMapper _mapper = new EntityMapper();

        public FlightRepository(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

        public async Task<int> GetLastIdAsync()
        {
            var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AirportDbContext>();
            var lastEntity = await dbContext.Flights.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
            return lastEntity!.Id;
        }

        public async Task SaveFlight(Flight flight)
        {
            var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AirportDbContext>();
            await dbContext.Flights.AddAsync(_mapper.ModelToEntity(flight));
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateFlight(Flight flight)
        {
            var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AirportDbContext>();
            dbContext.Flights.Update(_mapper.ModelToEntity(flight));
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteFlight(Flight flight)
        {
            var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AirportDbContext>();
            dbContext.Flights.Remove(_mapper.ModelToEntity(flight));
            await dbContext.SaveChangesAsync();
        }
    }
}
