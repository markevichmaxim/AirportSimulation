using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IFlightRepository
    {
        Task<int> GetLastIdAsync();
        Task SaveFlight(Flight flight);
        Task UpdateFlight(Flight flight);
        Task DeleteFlight(Flight flight);
    }
}
