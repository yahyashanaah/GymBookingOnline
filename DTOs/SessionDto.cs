namespace GymBooking.API.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }

        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
    }
}
