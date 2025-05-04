namespace GymBooking.API.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        // Relationships
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
