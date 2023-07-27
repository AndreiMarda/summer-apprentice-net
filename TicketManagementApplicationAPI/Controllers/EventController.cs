using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<EventController> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = _mapper.Map<List<EventDto>>(events);

            return Ok(dtoEvents);

            //var dtoEvents = new List<EventDto>();

            //foreach (var @event in events)
            //{
            //    var eventDto = new EventDto();

            //    eventDto = _mapper.Map<EventDto>(@event);

            //    dtoEvents.Add(eventDto);
            //}
            // return Ok(dtoEvents);
        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var @event = await _eventRepository.GetById(id);
            
            //if(@event == null)
            //{
            //    return NotFound();
            //}            
            var eventDto = _mapper.Map<EventDto>(@event);
            
            return Ok(eventDto);

        }
        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            if(eventPatch == null) 
                throw new ArgumentNullException(nameof(eventPatch));
            
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if(eventEntity == null)
            {
                return NotFound();
            }

            if(eventPatch.EventName.IsNullOrEmpty()) 
                eventEntity.EventName = eventPatch.EventName;
            if(eventPatch.EventDescription.IsNullOrEmpty()) 
                eventEntity.EventDescription = eventPatch.EventDescription;
            
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }

        [HttpDelete] 
        public async Task<ActionResult> Delete(int id) 
        {
            var eventEntity = await _eventRepository.GetById(id);

            if( eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}
