using Core.Models.Enums.Airport;

namespace Core.Interfaces.Services
{
    public interface IStateDataLoader
    {
        Task<Dictionary<Facility, bool>> LoadQueuesState();
        Task<bool> LoadRunwayState();
        Task<Dictionary<Gateway, bool>> LoadTerminalState();
    }
}
