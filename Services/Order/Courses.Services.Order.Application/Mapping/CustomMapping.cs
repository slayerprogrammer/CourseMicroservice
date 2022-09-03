using AutoMapper;
using Courses.Services.Order.Application.Dtos;

namespace Courses.Services.Order.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Order.Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<Order.Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Order.Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
        }
    }
}