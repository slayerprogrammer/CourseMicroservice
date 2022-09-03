using Courses.Services.Order.Application.Dtos;
using Courses.Services.Order.Application.Mapping;
using Courses.Services.Order.Application.Queries;
using Courses.Services.Order.Infrastructure;
using Courses.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Courses.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems)
                                              .Where(x => x.BuyerId == request.UserId)
                                              .ToListAsync();

            //AutoMapper da boş geleni dönüştürmede sorun yaşamamak adına datanın null gelme durumuna bakmakta fayda var.
            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}