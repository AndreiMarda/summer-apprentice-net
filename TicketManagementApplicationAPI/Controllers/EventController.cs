using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
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
        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();
            var dtoEvents = new List<EventDto>();

            foreach(var @event in events)
            {
                var eventDto = new EventDto();
                //{
                //    EventId = @event.EventId,
                //    EventDescription = @event.EventDescription,
                //    EventName = @event.EventName,
                //    EventType = @event.EventType?.Name ?? string.Empty,
                //    Venue = @event.Venue.Location ?? string.Empty
                //};
                eventDto = _mapper.Map<EventDto>(@event);

                dtoEvents.Add(eventDto);
            }

            //Linq
            //var dtoEventsLinq = events.Select(e => new EventDto()
            //{
            //    EventId = e.EventId,
            //    EventDescription = e.EventDescription,
            //    EventName = e.EventName,
            //    EventType = e.EventType?.Name ?? string.Empty,
            //    Venue = e.Venue.Location ?? string.Empty
            //});

            return Ok(dtoEvents);
        }

        [HttpGet]
        public ActionResult<EventDto> GetById(int id)
        {
            var @event = _eventRepository.GetById(id);
            
            if(@event == null)
            {
                return NotFound();
            }            
            var eventDto = _mapper.Map<EventDto>(@event);
            
            return Ok(eventDto);


        }
        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            var eventEntity = _eventRepository.GetById(eventPatch.EventId);
            if(eventEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(await eventEntity); //aici pune await
            return Ok(eventEntity);
        }
        [HttpDelete] public async Task<ActionResult> Delete(int id) 
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
