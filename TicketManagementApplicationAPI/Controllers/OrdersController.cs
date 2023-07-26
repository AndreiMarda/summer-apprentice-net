using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TicketManagementApplicationAPI.Model;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice,
                OrderedAt = o.OrderedAt

            });


            return Ok(dtoOrders);
        }
        [HttpGet]
        public ActionResult<OrderDto> GetById(int id)
        {
            var @order = _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            var dtoOrder = new OrderDto()
            {
                OrderId = @order.OrderId,
                NumberOfTickets = @order.NumberOfTickets,
                TotalPrice = @order.TotalPrice,
                OrderedAt = @order.OrderedAt
            };

            return Ok(dtoOrder);
        }
    }
}