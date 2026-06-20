using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace try4.Models
{
    public class Rout
    {
        [Key]
        public int RouteId { get; set; }

        [Column("CenterAId")]
        public int CenterAid { get; set; }

        [Column("CenterBId")]
        public int CenterBid { get; set; }

        public int MinCenterId { get; set; }

        public int MaxCenterId { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string RoadType { get; set; }

        public int DurationMinutes { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CenterAid")]
        [InverseProperty("RouteCenterAs")]
        public virtual Center CenterA { get; set; } = null!;

        [ForeignKey("CenterBid")]
        [InverseProperty("RouteCenterBs")]
        public virtual Center CenterB { get; set; } = null!;

        [InverseProperty("Rout")]
        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
