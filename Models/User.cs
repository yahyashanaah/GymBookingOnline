using System.ComponentModel.DataAnnotations;

namespace GymBooking.API.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(Admin|Member)$", ErrorMessage = "Role must be either Admin or Member")]
        public string? Role { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
