using Core.Models.Enums.Airport;
using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IFacilityRepository
    {
        Task<bool> AnyFlightsInDb();
        Task<Queue<Flight>> GetQueue(Facility facility);
        Task<Flight?> GetRunway(Facility facility);
        Task<List<Flight>> GetTerminal(Facility facility);
    }
}
