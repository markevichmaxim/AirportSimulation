using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Enums.Airport;
using Service.Airport.State;

namespace Service.Airport.Data
{
    public class StateDataLoader : IStateDataLoader
    {
        private readonly AirportState _airportState;
        private readonly IFacilityRepository _repository;

        public StateDataLoader(AirportState airportState, IFacilityRepository facilityRepository)
        {
            _airportState = airportState;
            _repository = facilityRepository;
        }

        public async Task<Dictionary<Facility, bool>> LoadQueuesState()
        {
            var result = new Dictionary<Facility, bool>()
            {
                { Facility.LandingQueue, false },
                { Facility.BoardingQueue, false },
                { Facility.TakingoffQueue, false },
                { Facility.DepartingQueue, false },
            };

            if (!await _repository.AnyFlightsInDb())
                return result;

            var facilities = Enum.GetValues<Facility>();
            foreach (var facility in facilities)
            {
                if (facility == Facility.Runway || facility == Facility.Terminal)
                    continue;

                var queue = await _repository.GetQueue(facility);
                if (queue.Any())
                {
                    _airportState.Areas.Queues[facility] = queue;
                    result[facility] = true;
                }
            }

            return result;
        }

        public async Task<bool> LoadRunwayState()
        {
            if (!await _repository.AnyFlightsInDb())
                return false;

            var flight = await _repository.GetRunway(Facility.Runway);
            if (flight != null)
            {
                _airportState.Runway.Flight = flight;
                return true;
            }

            return false;
        }

        public async Task<Dictionary<Gateway, bool>> LoadTerminalState()
        {
            var result = new Dictionary<Gateway, bool>()
            {
                {Gateway.FirstGateway, false},
                {Gateway.SecondGateway, false}
            };

            if (!await _repository.AnyFlightsInDb())
                return result;

            List<Flight> terminalFlights = await _repository.GetTerminal(Facility.Terminal);
            if (terminalFlights.Count > 0)
            {
                int currentGateway = 1;
                for (int i = 0; i < terminalFlights.Count; i++)
                {
                    if (currentGateway > 2)
                        break;

                    _airportState.Terminal.Gateways[(Gateway)currentGateway] = terminalFlights[i];
                    result[(Gateway)currentGateway] = true;
                    currentGateway++;
                }
            }

            return result;
        }
    }
}
