using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace try4.Models
{
    public enum UserRole
    {
        User = 0,
        Admin =1
        
    }
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(100)]
        public string UserFullName { get; set; } = null!;
        [Required]
        [StringLength(150)]
      //  public string Email { get; set; }
        public UserRole Role { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        [InverseProperty("User")]
        public virtual ICollection<Search> Searches { get; set; } = new HashSet<Search>();


    }
}
