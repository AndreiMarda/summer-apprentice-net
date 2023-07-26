namespace TicketManagementApplicationAPI.Model.Dto
{
    public class OrderPatchDto
    {
        public int OrderId { get; set; }
        public DateTime OrderedAt { get; set; }
        public int NumberOfTickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
