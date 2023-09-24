using Core.Interfaces.Services;

namespace Service.Airport.State
{
    public class StateEvents
    {
        public event EventHandler RunwayFree = delegate { };
        public event EventHandler TerminalFree = delegate { };
        public event EventHandler LeavingAirport = delegate { };

        public void OnRunwayFree() => RunwayFree?.Invoke(this, EventArgs.Empty);
        public void OnTerminalFree() => TerminalFree?.Invoke(this, EventArgs.Empty);
        public void OnLeavingAirport() => LeavingAirport?.Invoke(this, EventArgs.Empty);

    }
}
