using API.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class FlightDto
    {
        [Required]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Serial Number must be between 4 and 6 characters.")]
        public string? SerialNumber { get; set; }
        [Required]
        [ValidDateTimeWithTime(ErrorMessage = "Arrival Time must include both date and time components.")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [ValidDateTimeWithTime(ErrorMessage = "Departure Time must contain both date and time components.")]
        public DateTime DepartureTime { get; set; }
    }
}
