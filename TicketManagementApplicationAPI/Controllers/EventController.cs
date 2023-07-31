using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicketManagementApplicationAPI.Exceptions;
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
        //private readonly ILogger _logger;
        public EventController(IEventRepository eventRepository, IMapper mapper/*, ILogger<EventController> logger*/)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet(Name = "GetAllEvents")]
        public ActionResult<IEnumerable<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = events.Select(e => _mapper.Map<EventDto>(e));
            return Ok(dtoEvents);
            //var dtoEvents = _mapper.Map<List<EventDto>>(events);

            //await Task.Delay(TimeSpan.FromSeconds(20)); // Intarziere 20 sec

            //var eventDto = _mapper.Map<List<EventDto>>(events);

            //return Ok(eventDto);

            //var dtoEvents = new List<EventDto>();

            //foreach (var @event in events)
            //{
            //    var eventDto = new EventDto();

            //    eventDto = _mapper.Map<EventDto>(@event);

            //    dtoEvents.Add(eventDto);
            //}
            // return Ok(dtoEvents);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            try
            {
                var @event = await _eventRepository.GetById(id);

                return Ok(_mapper.Map<EventDto>(@event));
            }
            catch (EntityNotFoundException ex) 
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            //var @eventDto = _mapper.Map<EventDto>(@event);

            //if(@event == null)
            //{
            //    return NotFound();
            //}            
            //var eventDto = _mapper.Map<EventDto>(@event);
        }

        [HttpPatch("{id}", Name = "PatchEvent")]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            if (eventPatch == null)
                throw new ArgumentNullException(nameof(eventPatch));

            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if (eventEntity == null)
            {
                return NotFound();
            }

            if (eventPatch.EventName.IsNullOrEmpty())
                eventEntity.EventName = eventPatch.EventName;
            if (eventPatch.EventDescription.IsNullOrEmpty())
                eventEntity.EventDescription = eventPatch.EventDescription;

            _mapper.Map(eventPatch, eventEntity);

            _eventRepository.Update(eventEntity);

            return Ok(eventEntity);
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        public async Task<ActionResult> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }
    }
}