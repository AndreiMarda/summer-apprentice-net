using Microsoft.EntityFrameworkCore;
using TicketManagementApplicationAPI.Exceptions;
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
        public async Task Add(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            //var orders = _dbContext.Orders;
            return _dbContext.Orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();
           
            if (order == null)
                throw new EntityNotFoundException(id, nameof(Order));

            return order;
        }

        public async Task Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(Order order)
        {
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
