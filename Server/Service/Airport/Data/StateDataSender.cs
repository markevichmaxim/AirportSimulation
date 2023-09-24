using Core.Interfaces.Services;
using Core.Models;
using Service.Airport.State;

namespace Service.Airport.Data
{
    public class StateDataSender : IStateDataSender
    {
        private readonly AirportState _state;

        public StateDataSender(AirportState airportState) => _state = airportState;

        public List<Flight> GetAirportStateImage()
        {
            List<Flight> flights = new List<Flight>();

            foreach (var queue in _state.Areas.Queues)
                flights.AddRange(queue.Value);

            foreach (var gateway in _state.Terminal.Gateways)
            {
                if (gateway.Value != null)
                    flights.Add(gateway.Value);
            }

            if (_state.Runway.Flight != null)
                flights.Add(_state.Runway.Flight);

            return flights;
        }
    }
}
