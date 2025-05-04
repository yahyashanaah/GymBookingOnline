namespace GymBooking.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        // Relationships
        public int UserId { get; set; }
        public User? User { get; set; }

        public int SessionId { get; set; }
        public Session? Session { get; set; }

        public DateTime BookingTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Confirmed";
    }
}
