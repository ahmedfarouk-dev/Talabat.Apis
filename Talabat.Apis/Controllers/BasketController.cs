using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepositorycs _basket;

        public BasketController(IBasketRepositorycs basket)
        {
            _basket = basket;
        }

        [HttpDelete]

        public async Task<bool> Delete(string id)
        {
            return await _basket.DeleteBasketAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreateOrUp = await _basket.UpdateBasketAsync(basket);

            if (CreateOrUp == null)
                return BadRequest(new ApiResponse(404));

            return Ok(CreateOrUp);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var Basket = await _basket.GetBasketAsync(id);

            return Basket is null ? new CustomerBasket(id) : Basket;
        }
    }
}
