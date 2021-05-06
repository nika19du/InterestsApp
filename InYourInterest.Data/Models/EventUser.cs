namespace InYourInterest.Data.Models
{
    public class EventUser
    {
        public string EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
