using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Services;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IpaymentServices services;

        public PaymentsController(IpaymentServices services)
        {
            this.services = services;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePayment(string basketId)
        {

            var basket = await services.CreateOrUpdate(basketId);

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }
    }
}
