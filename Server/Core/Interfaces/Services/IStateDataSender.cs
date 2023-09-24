using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IStateDataSender
    {
        List<Flight> GetAirportStateImage();
    }
}
