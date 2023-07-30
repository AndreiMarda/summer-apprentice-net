using AutoMapper;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Model.Dto;

namespace TicketManagementApplicationAPI.Profiles
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<Venue, VenuePatchDto>().ReverseMap();
        }
    }

}
