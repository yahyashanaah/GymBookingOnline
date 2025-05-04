namespace GymBooking.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // "User" or "Admin"

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
