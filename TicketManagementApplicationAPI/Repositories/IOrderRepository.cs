using TicketManagementApplicationAPI.Model;
namespace TicketManagementApplicationAPI.Repositories
{
    public interface IOrderRepository
    {
        Task <IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task Add(Order order);
        Task Update(Order order);
        Task Delete(Order order);

    }
}
