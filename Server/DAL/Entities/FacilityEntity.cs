using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Facilities")]
    public class FacilityEntity
    {
        [Key]
        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }

        public virtual ICollection<FlightEntity>? Flights { get; set; }
    }
}
