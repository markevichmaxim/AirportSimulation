using Core.Models.Enums.Airport;
using Core.Models.Enums.Flight;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Flight
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("arrivalTime")]
        public DateTime ArrivalTime { get; set; }

        [JsonPropertyName("departureTime")]
        public DateTime DepartureTime { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("facility")]
        public Facility Facility { get; set; }
    }
}
