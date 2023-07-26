using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        int Add(Order order);
        void Update(Order order);
        int Delete(int id);

    }
}
