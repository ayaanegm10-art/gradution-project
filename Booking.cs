using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace try4.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int TripId { get; set; }

        public int UserId { get; set; }

        //[Required]
        //[StringLength(100)]
        //public string UserName { get; set; }

        public int SeatsCount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime BookingTime { get; set; }

        [ForeignKey("TripId")]
        [InverseProperty("Bookings")]
        public virtual Trip Trip { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("Bookings")]
        public virtual User User { get; set; } = null!;
    }
}
