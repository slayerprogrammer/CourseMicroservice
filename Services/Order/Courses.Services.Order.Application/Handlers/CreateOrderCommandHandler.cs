using Courses.Services.Order.Application.Commands;
using Courses.Services.Order.Application.Dtos;
using Courses.Services.Order.Domain.OrderAggregate;
using Courses.Services.Order.Infrastructure;
using Courses.Shared.Dtos;
using MediatR;

namespace Courses.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Province,
                                         request.AddressDto.District,
                                         request.AddressDto.Street,
                                         request.AddressDto.ZipCode,
                                         request.AddressDto.Line);

            //C# 8.0 ile gelen özellik sol tarafta tipini verip
            //new anahtar sözcüğü ile kullanılabilir
            //OrderDto orderDto = new();

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}