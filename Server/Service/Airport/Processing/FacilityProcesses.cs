using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.Enums.Airport;
using Core.Models.Enums.Flight;
using Service.Airport.Data;
using Service.Airport.State;

namespace Service.Airport.Processing
{
    /// <summary>
    /// Manages various airport facility processes, including runway, terminal, and departure processing.
    /// </summary>
    public class FacilityProcesses
    {
        private readonly TimeSpan RUNWAY_PROCESS_TIME = TimeSpan.FromSeconds(6.5);
        private readonly TimeSpan TERMINAL_PROCESS_TIME = TimeSpan.FromSeconds(16);
        private readonly TimeSpan TRANSFER_PROCESS_TIME = TimeSpan.FromSeconds(0.6);
        private readonly TimeSpan RELEASE_PROCESS_TIME = TimeSpan.FromSeconds(4.5);

        private readonly StateManager _stateManager;
        private readonly FlightManager _flightManager;
        private readonly IFlightRepository _repository;
        private readonly StateEvents _stateEvents;
        private readonly DataEvents _dataEvents;

        public FacilityProcesses(StateManager stateManager, IFlightRepository repository, StateEvents stateEvents, DataEvents dataEvents)
        {
            _stateManager = stateManager;
            _repository = repository;
            _stateEvents = stateEvents;
            _dataEvents = dataEvents;
            _flightManager = new FlightManager();
        }

        public async Task ProcessRunway()
        {
            await Task.Delay(RUNWAY_PROCESS_TIME);

            _ = TransferFlight(_stateManager.GetRunwayFlight());
            _stateManager.RemoveFlightFromRunway();
        }

        public async Task ProcessTerminal(Gateway gateway)
        {
            await Task.Delay(TERMINAL_PROCESS_TIME);

            _ = TransferFlight(_stateManager.GetGatewayFlight(gateway));
            _stateManager.RemoveFlightFromGateway(gateway);
        }

        public void ProcessQueues(Dictionary<Facility, bool> queues)
        {
            if (queues[Facility.LandingQueue] || queues[Facility.TakingoffQueue])
                _stateEvents.OnRunwayFree();

            if (queues[Facility.BoardingQueue])
                _stateEvents.OnTerminalFree();

            if (queues[Facility.DepartingQueue])
                _stateEvents.OnLeavingAirport();
        }

        public async Task ProcessDeparture()
        {
            await Task.Delay(RELEASE_PROCESS_TIME);
        }

        private async Task TransferFlight(Flight flight)
        {
            await Task.Delay(TRANSFER_PROCESS_TIME);

            _flightManager.SwitchStage(flight);

            _stateManager.AddFlightToQueue(flight.Facility, flight);
            await _repository.UpdateFlight(flight);

            _dataEvents.NotifyClientsAboutChange(flight);

            if (flight.Status == Status.WaitingToBoard)
            {
                if (_stateManager.IsGatewayFree())
                    _stateManager.NotifyTerminalClearance();
            }

            if (flight.Status == Status.WaitingToTakeoff)
            {
                if (_stateManager.IsRunwayClear())
                    _stateManager.NotifyRunwayClearance();
            }

            if (flight.Status == Status.WaitingToDeparture)
                _stateManager.NotifyFlightDeparture();
        }
    }
}
