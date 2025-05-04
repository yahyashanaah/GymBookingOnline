namespace GymBooking.API.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string SessionName { get; set; } = string.Empty;
        public DateTime BookingTime { get; set; }
        public string Status { get; set; } = "Confirmed";
    }
}
