using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.Enums.Airport;
using Service.Airport.Data;
using Service.Airport.State;

namespace Service.Airport.Processing
{
    /// <summary>
    /// The main bussines logic of airport with database interaction
    /// </summary>
    public class AirportManager
    {
        private readonly StateManager _stateManager;
        private readonly FacilityProcesses _facilityProcesses;
        private readonly FlightManager _flightManager;
        private readonly IFlightRepository _repository;
        private readonly DataEvents _dataEvents;

        public AirportManager(StateManager stateManager, FacilityProcesses facilityProcesses, IFlightRepository flightRepository, DataEvents dataEvents)
        {
            _stateManager = stateManager;
            _facilityProcesses = facilityProcesses;
            _repository = flightRepository;
            _dataEvents = dataEvents;
            _flightManager = new FlightManager();
        }

        public async Task AcceptFlightAtAirport(Flight incomeFlight)
        {
            _flightManager.SwitchStage(incomeFlight);

            await _repository.SaveFlight(incomeFlight);

            incomeFlight.Id = await _repository.GetLastIdAsync();

            _stateManager.AddFlightToQueue(incomeFlight.Facility, incomeFlight);

            _dataEvents.NotifyClientsAboutChange(incomeFlight);

            if (_stateManager.IsRunwayClear())
                _stateManager.NotifyRunwayClearance();
        }

        public async Task AcceptFlightOnRunway()
        {
            if (!_stateManager.IsRunwayClear())
                return;

            Facility priorityQueue = _stateManager.DefinePriorityQueueForRunway();
            if (priorityQueue == default)
                return;

            Flight flight = _stateManager.GetFlightFromQueue(priorityQueue);

            _flightManager.SwitchStage(flight);

            _stateManager.PutFlightOnRunway(flight);

            await _repository.UpdateFlight(flight);

            _dataEvents.NotifyClientsAboutChange(flight);

            await _facilityProcesses.ProcessRunway();

            if (_stateManager.AnyFlightsInQueue(Facility.LandingQueue) || _stateManager.AnyFlightsInQueue(Facility.TakingoffQueue))
                _stateManager.NotifyRunwayClearance();
        }

        public async Task AcceptFlightToTerminal()
        {
            if (!_stateManager.AnyFlightsInQueue(Facility.BoardingQueue))
                return;

            Gateway currentGateway = _stateManager.GetFreeGateway();
            if (currentGateway == default)
                return;

            Flight flight = _stateManager.GetFlightFromQueue(Facility.BoardingQueue);

            _flightManager.SwitchStage(flight);

            _stateManager.DockFlightAtGateway(currentGateway, flight);

            await _repository.UpdateFlight(flight);

            _dataEvents.NotifyClientsAboutChange(flight);

            await _facilityProcesses.ProcessTerminal(currentGateway);

            if (_stateManager.AnyFlightsInQueue(Facility.BoardingQueue))
                _stateManager.NotifyTerminalClearance();
        }

        public async Task ReleaseFlightFromAirport()
        {
            if (!_stateManager.AnyFlightsInQueue(Facility.DepartingQueue))
                return;

            await _facilityProcesses.ProcessDeparture();

            var flightDropped = _stateManager.GetFlightFromQueue(Facility.DepartingQueue);

            flightDropped.Status = 0;

            await _repository.DeleteFlight(flightDropped);

            _dataEvents.NotifyClientsAboutChange(flightDropped);
        }
    }
}
