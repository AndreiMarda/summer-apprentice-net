using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementApplicationContext _dbContext;
        public OrderRepository()
        {
            _dbContext = new TicketManagementApplicationContext();
        }
        public int Add(Order order)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            _dbContext.Remove(id);
            _dbContext.SaveChanges();
            return 0;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public Order GetById(int id)
        {
            var order = _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefault();

            return order;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
