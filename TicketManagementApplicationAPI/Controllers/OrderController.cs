using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        //private readonly ILogger _logger;
        public OrderController(IOrderRepository orderRepository, IMapper mapper/*, ILogger logger*/)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll()
        {
            var @order = _orderRepository.GetAll();

            //var dtoOrdersMapper = _mapper.Map<IEnumerable<OrderDto>>(@order);
            var dtoOrders = @order.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice,
                OrderedAt = o.OrderedAt

            });

            return Ok(dtoOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            if (orderPatch == null)
                throw new ArgumentNullException(nameof(orderPatch));

            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);


            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderPatch.NumberOfTickets != 0)
                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            if (orderPatch.TotalPrice != 0)
                orderEntity.TotalPrice = orderPatch.TotalPrice;

            _mapper.Map(orderPatch, orderEntity);
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}