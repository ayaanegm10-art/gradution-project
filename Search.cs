using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace try4.Models
{
    public class Search
    {
        [Key]
        public int SearchId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int FromId { get; set; }
        public int ToId { get; set; }
        public int Seats { get; set; }
        public DateTime SearchDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("FromId")]
        public virtual Center FromCenter { get; set; } = null!;

        [ForeignKey("ToId")]
        public virtual Center ToCenter { get; set; } = null!;
    }
}
