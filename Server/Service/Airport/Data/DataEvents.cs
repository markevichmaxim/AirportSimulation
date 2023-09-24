using Core.Models;

namespace Service.Airport.Data
{
    public class DataEvents
    {
        public event Action<Flight> DataUpdated = delegate { };
        public void NotifyClientsAboutChange(Flight updatedFlight) => DataUpdated.Invoke(updatedFlight);
    }
}
