using AutoMapper;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Model.Dto;

namespace TicketManagementApplicationAPI.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile() 
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, EventPatchDto>().ReverseMap();
        }   
    }
}
