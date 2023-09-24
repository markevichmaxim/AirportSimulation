using Core.Models.Enums.Airport;
using Core.Models.Locations;

namespace Service.Airport.State
{
    public class AirportState
    {
        public Areas Areas { get; } = new Areas();
        public Terminal Terminal { get; } = new Terminal();
        public Runway Runway { get; } = new Runway();
        public Dictionary<Facility, object> Lockers { get; } = new Dictionary<Facility, object>()
        {
            { Facility.LandingQueue, new object() },
            { Facility.TakingoffQueue, new object() },
            { Facility.BoardingQueue, new object() },
            { Facility.DepartingQueue, new object() },
        };
    }
}
