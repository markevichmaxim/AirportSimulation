using Core.Models.Enums.Airport;

namespace Core.Models.Locations
{
    public class Terminal
    {
        public readonly Facility Facility = Facility.Terminal;
        public Dictionary<Gateway, Flight?> Gateways { get; } = new Dictionary<Gateway, Flight?>() 
        {
            {Gateway.FirstGateway, null},
            {Gateway.SecondGateway, null},
        };
    }
}
