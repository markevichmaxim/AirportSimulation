using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Flights")]
    public class FlightEntity
    {
        [Key]
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public int Status { get; set; }

        [ForeignKey("FacilityId")]
        public int FacilityId { get; set; }
        public virtual FacilityEntity? Facility { get; set; }
    }
}
