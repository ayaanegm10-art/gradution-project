using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace try4.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        public double TripPrice { get; set; }

        public int RouteId { get; set; }

        public int FromCenterId { get; set; }

        public int ToCenterId { get; set; }

        public TimeOnly DepartureTime { get; set; }

        public DateTime? DepartureDate { get; set; }

        public int TotalSeats { get; set; }

        public int BookedSeats { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

        // Navigation properties
        [ForeignKey("RouteId")]
        [InverseProperty("Trips")]
        public virtual Rout Rout { get; set; } = null!;

        [ForeignKey("FromCenterId")]
        [InverseProperty("TripFromCenters")]
        public virtual Center FromCenter { get; set; } = null!;

        [ForeignKey("ToCenterId")]
        [InverseProperty("TripToCenters")]
        public virtual Center ToCenter { get; set; } = null!;
    }
}
