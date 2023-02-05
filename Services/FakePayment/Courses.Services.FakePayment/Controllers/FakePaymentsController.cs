using Courses.Shared.ControllerBases;
using Courses.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}