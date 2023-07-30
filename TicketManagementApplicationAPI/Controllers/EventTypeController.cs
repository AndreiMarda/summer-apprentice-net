using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EventTypeController(IEventTypeRepository eventTypeRepository, IMapper mapper, ILogger logger)
        {
            _eventTypeRepository = eventTypeRepository ?? throw new ArgumentNullException(nameof(eventTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet(Name = "GetAllEventTypes")]
        public async Task <IEnumerable<EventTypeDto>> GetAll()
        {
            var eventType = await _eventTypeRepository.GetAll();

            var dtoEventTypeMapper = _mapper.Map<IEnumerable<EventTypeDto>>(eventType);

            return (IEnumerable<EventTypeDto>)Ok(dtoEventTypeMapper);
        }

        [HttpGet("{id}", Name = "GetEventTypesById")]
        public async Task<ActionResult<EventTypeDto>> GetById(int id)
        {
            var eventType = await _eventTypeRepository.GetById(id);

            var eventTypeDto = _mapper.Map<EventTypeDto>(eventType);

            return Ok(eventTypeDto);
        }

        [HttpPatch("{id}", Name = "PatchEventType")]
        public async Task<ActionResult<EventTypePatchDto>> Patch(EventTypePatchDto eventTypePatch)
        {
            if (eventTypePatch == null)
                throw new ArgumentNullException(nameof(eventTypePatch));

            var eventTypeEntity = await _eventTypeRepository.GetById(eventTypePatch.EventTypeId);

            if (eventTypeEntity == null)
            {
                return NotFound();
            }

            if (eventTypePatch.Name.Equals(null))
                eventTypeEntity.Name = eventTypePatch.Name;

            _eventTypeRepository.Update(eventTypeEntity);
            _mapper.Map(eventTypePatch, eventTypeEntity);
            return Ok(eventTypeEntity);
        }

        [HttpDelete("{id}", Name = "DeleteEventTypeById")]
        public async Task<ActionResult> Delete(int id)
        {
            var eventTypeEntity = await _eventTypeRepository.GetById(id);
            
            if (eventTypeEntity == null)
            {
                return NotFound();
            }
            _eventTypeRepository.Delete(eventTypeEntity);
            return NoContent();
        }
    }
}