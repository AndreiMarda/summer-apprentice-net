using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var @order = _orderRepository.GetAll();

            var dtoOrdersMapper = _mapper.Map<List<OrderDto>>(@order);

            return Ok(dtoOrdersMapper);
            
            //var dtoOrders = @order.Select(o => new OrderDto()
            //{
            //    OrderId = o.OrderId,
            //    NumberOfTickets = o.NumberOfTickets,
            //    TotalPrice = o.TotalPrice,
            //    OrderedAt = o.OrderedAt
            //});
            //var dtoOrders = _mapper.Map<OrderDto>(@order);
            //return Ok(dtoOrders);
        }
  
        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            //var dtoOrder = new OrderDto()
            //{
            //    OrderId = @order.OrderId,
            //    NumberOfTickets = @order.NumberOfTickets,
            //    TotalPrice = @order.TotalPrice,
            //    OrderedAt = @order.OrderedAt
            //};
            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(orderPatch, orderEntity);
            if (orderPatch.NumberOfTickets != 0)
                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;

            _orderRepository.Update(orderEntity);

            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            if(orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}