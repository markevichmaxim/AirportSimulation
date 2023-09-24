using Core.Models.Enums.Airport;

namespace Core.Models.Locations
{
    public class Runway
    {
        public readonly Facility Facility = Facility.Runway;
        public Flight? Flight { get; set; } = null;
    }
}
