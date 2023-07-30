using System.Data;
namespace TicketManagementApplicationAPI.Model.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderedAt { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategory { get; set; }
    }
}
