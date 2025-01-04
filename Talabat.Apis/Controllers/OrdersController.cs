using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto order)
        {
            var Order = await _service.CreateOrder(order.BuyerEmail, order.BasketId, order.DeliveryMethodId, order.ShippingAddress);


            if (Order == null)
                return BadRequest(404);

            return Ok(Order);
        }
    }
}
