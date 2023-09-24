using Core.Models;
using Core.Models.Enums.Airport;

namespace Service.Airport.State
{
    /// <summary>
    /// Encapsulation layer for interaction with the Airport state
    /// </summary>
    public class StateManager
    {
        private readonly AirportState _airport;
        private readonly StateEvents _stateEvents;

        public StateManager(AirportState airportState, StateEvents stateEvents)
        {
            _airport = airportState;
            _stateEvents = stateEvents;
        }

        public void NotifyRunwayClearance() => _stateEvents.OnRunwayFree();
        public void NotifyTerminalClearance() => _stateEvents.OnTerminalFree();
        public void NotifyFlightDeparture() => _stateEvents.OnLeavingAirport();

        public void AddFlightToQueue(Facility queue, Flight flight)
        {
            lock (_airport.Lockers[queue])
                _airport.Areas.Queues[queue].Enqueue(flight);
        }
        public Flight GetFlightFromQueue(Facility queue)
        {
            lock (_airport.Lockers[queue])
                return _airport.Areas.Queues[queue].Dequeue();
        }
        public bool AnyFlightsInQueue(Facility queue)
        {
            lock (_airport.Lockers[queue])
                return _airport.Areas.Queues[queue].Any();
        }

        public bool IsRunwayClear() => _airport.Runway.Flight == null;
        public void PutFlightOnRunway(Flight flight) => _airport.Runway.Flight = flight;
        public void RemoveFlightFromRunway() => _airport.Runway.Flight = null;
        public Flight GetRunwayFlight() => _airport.Runway.Flight!;

        public Facility DefinePriorityQueueForRunway()
        {
            const Facility landingQueue = Facility.LandingQueue;
            const Facility takingoffQueue = Facility.TakingoffQueue;

            DateTime? arrivalTime = null;
            DateTime? departureTime = null;
            lock (_airport.Lockers[landingQueue])
                arrivalTime = _airport.Areas.Queues[landingQueue].FirstOrDefault()?.ArrivalTime;
            lock (_airport.Lockers[takingoffQueue])
                departureTime = _airport.Areas.Queues[takingoffQueue].FirstOrDefault()?.DepartureTime;

            if (arrivalTime.HasValue && (!departureTime.HasValue || arrivalTime <= departureTime))
                return Facility.LandingQueue;

            if (departureTime.HasValue)
                return Facility.TakingoffQueue;

            return default;
        }

        public bool IsGatewayFree() => _airport.Terminal.Gateways.Values.Any(gate => gate == null);
        public Gateway GetFreeGateway() => _airport.Terminal.Gateways.FirstOrDefault(gateway => gateway.Value == null).Key;
        public void DockFlightAtGateway(Gateway gateway, Flight flight) => _airport.Terminal.Gateways[gateway] = flight;
        public void RemoveFlightFromGateway(Gateway gateway) => _airport.Terminal.Gateways[gateway] = null;
        public Flight GetGatewayFlight(Gateway gateway) => _airport.Terminal.Gateways[gateway]!;
    }
}
