using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IVenueRepository _venueRepository;
        public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger logger, 
            IEventRepository eventRepository, IEventTypeRepository eventTypeRepository, IVenueRepository venueRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _eventTypeRepository = eventTypeRepository ?? throw new ArgumentNullException(nameof(eventTypeRepository));
            _venueRepository = _venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var @order = await _orderRepository.GetAll();

            var dtoOrdersMapper = _mapper.Map<IEnumerable<OrderDto>>(@order);

            return Ok(dtoOrdersMapper);
        }
  
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            if(@order == null)
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

            await _orderRepository.GetAll();
            await _venueRepository.GetAll();
            await _eventTypeRepository.GetAll();

            if (orderEntity == null)
            {
                return NotFound();
            }
            
            if (orderPatch.NumberOfTickets != 0)
                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            if (orderPatch.TotalPrice != 0)
                orderEntity.TotalPrice = orderPatch.TotalPrice;

            _mapper.Map(orderPatch, orderEntity);
            await _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete("{id}")]
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