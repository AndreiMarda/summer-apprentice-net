﻿using Microsoft.EntityFrameworkCore;
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
        public void Add(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();

            return order;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void Delete(Order order)
        {
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }
    }
}
