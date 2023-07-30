namespace TicketManagementApplicationAPI.Model.Dto
{
    public class VenuePatchDto
    {
        public int VenueId { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public int? Capacity { get; set; }
    }
}
