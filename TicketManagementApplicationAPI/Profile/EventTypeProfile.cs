using AutoMapper;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Model.Dto;

namespace TicketManagementApplicationAPI.Profiles
{
    public class EventTypeProfile : Profile
    {
        public EventTypeProfile() { 
        CreateMap<EventType, EventTypePatchDto>().ReverseMap();
        CreateMap<EventType, EventTypePatchDto>().ReverseMap();
        }
    }
}
