namespace GymBooking.API.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
