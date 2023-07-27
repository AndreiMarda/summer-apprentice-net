using AutoMapper;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Model.Dto;

namespace TicketManagementApplicationAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }
    }
}
