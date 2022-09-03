using Courses.Services.Order.Application.Commands;
using Courses.Services.Order.Application.Queries;
using Courses.Shared.ControllerBases;
using Courses.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityServer;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityServer)
        {
            _mediator = mediator;
            _sharedIdentityServer = sharedIdentityServer;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityServer.GetUserId });

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);

            return CreateActionResultInstance(response);
        }
    }
}