using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using TicketManagementApplicationAPI.Model;

namespace TicketManagementApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IMapper _mapper;
        public VenueController(IVenueRepository venueRepository, IMapper mapper)
        {
            _venueRepository = venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }


        [HttpGet(Name = "GetAllVenues")]
        public async Task<ActionResult<IEnumerable<VenueDto>>> GetAll()
        {
            var venues = await _venueRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<VenueDto>>(venues));
        }

        [HttpGet("{id}", Name = "GetVenue")]
        public async Task<ActionResult<Venue>> GetById(int id)
        {
            var venueDto = await _venueRepository.GetById(id);
            return Ok(_mapper.Map<Venue>(venueDto));
        }


        [HttpPatch("{id}", Name = "UpdateVenue")]
        public async Task<ActionResult<VenueDto>> Patch(VenuePatchDto venuePatchDto)
        {
            if (venuePatchDto == null)
                throw new ArgumentNullException(nameof(venuePatchDto));

            var venueEntity = await _venueRepository.GetById(venuePatchDto.VenueId);

            var venueDto = _mapper.Map<VenueDto>(venueEntity);

            if (!TryValidateModel(venueDto))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(venueDto, venueEntity);
            
            if(venuePatchDto.Location.IsNullOrEmpty())
                venuePatchDto.Location = venueEntity.Location;
            if(venuePatchDto.Capacity == null)
                venuePatchDto.Capacity = venueEntity.Capacity;

            _venueRepository.Update(venueEntity);
            return Ok(venueEntity);
        }


        [HttpDelete("{id}", Name = "DeleteVenueById")]
        public async Task<ActionResult> Delete(int id)
        {
            var venueDetails = await _venueRepository.GetById(id);

            if (venueDetails == null)
            {
                return NotFound();
            }
            _venueRepository.Delete(venueDetails);
            return NoContent();
        }
    }
}
