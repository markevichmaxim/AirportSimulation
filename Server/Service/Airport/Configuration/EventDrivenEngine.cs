using Core.Interfaces.Services;
using Service.Airport.Processing;
using Service.Airport.State;

namespace Service.Airport.Configuration
{
    /// <summary>
    /// Event-driven engine responsible for subscribing to and handling airport state events.
    /// </summary>
    public class EventDrivenEngine : IEventDrivenEngine
    {
        private readonly StateEvents _stateEvents;
        private readonly AirportManager _handler;

        public EventDrivenEngine(StateEvents stateEvents, AirportManager handler)
        {
            _stateEvents = stateEvents;
            _handler = handler;
        }

        public void SubscribeToEventHandlers()
        {
            _stateEvents.RunwayFree += async (sender, e) => await Task.Run(_handler.AcceptFlightOnRunway);

            _stateEvents.TerminalFree += async (sender, e) => await Task.Run(_handler.AcceptFlightToTerminal);

            _stateEvents.LeavingAirport += async (sender, e) => await Task.Run(_handler.ReleaseFlightFromAirport);
        }
    }
}
