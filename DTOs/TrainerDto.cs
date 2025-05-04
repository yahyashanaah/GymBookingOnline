namespace GymBooking.API.DTOs
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
}
// Compare this snippet from Models/Booking.cs: