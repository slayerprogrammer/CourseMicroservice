using Courses.Services.Order.Application.Dtos;
using Courses.Shared.Dtos;
using MediatR;

namespace Courses.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto AddressDto { get; set; }
    }
}