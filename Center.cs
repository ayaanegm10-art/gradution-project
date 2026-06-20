using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace try4.Models
{
    public class Center
    {

        [Key]
        public int CenterId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int? OrderIndex { get; set; }

        public bool IsActive { get; set; }

        [InverseProperty("CenterA")]
        public virtual ICollection<Rout> RouteCenterAs { get; set; } = new List<Rout>();

        [InverseProperty("CenterB")]
        public virtual ICollection<Rout> RouteCenterBs { get; set; } = new List<Rout>();

        [InverseProperty("FromCenter")]
        public virtual ICollection<Trip> TripFromCenters { get; set; } = new List<Trip>();

        [InverseProperty("ToCenter")]
        public virtual ICollection<Trip> TripToCenters { get; set; } = new List<Trip>();
    }
}
