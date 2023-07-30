namespace TicketManagementApplicationAPI.Model.Dto
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; }
        public EventType? EventType { get; set; }
        public Venue? Venue { get; set; }
    }
}
