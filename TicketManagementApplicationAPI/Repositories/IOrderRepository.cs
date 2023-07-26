using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Task <Order> GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);

    }
}
