namespace TicketManagementApplicationAPI.Model.Dto
{
    public class EventTypeDto
    {
        public int EventTypeId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
