using Core.Models.Enums.Airport;

namespace Core.Models.Locations
{
    public class Areas
    {
        public Dictionary<Facility, Queue<Flight>> Queues { get; } = new Dictionary<Facility, Queue<Flight>>()
        {
            { Facility.LandingQueue, new Queue<Flight>() },
            { Facility.BoardingQueue, new Queue<Flight>() },
            { Facility.TakingoffQueue, new Queue<Flight>() },
            { Facility.DepartingQueue, new Queue<Flight>() },
        };
    }
}
