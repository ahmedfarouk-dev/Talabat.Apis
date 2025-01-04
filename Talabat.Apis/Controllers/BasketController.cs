using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;

        public IMapper _Mapper { get; }

        public BasketController(IBasketRepository basket, IMapper mapper)
        {
            _basket = basket;
            _Mapper = mapper;
        }

        [HttpDelete]

        public async Task<bool> Delete(string id)
        {
            return await _basket.DeleteBasketAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var result = _Mapper.Map<CustomerBasket>(basket);

            var CreateOrUp = await _basket.UpdateBasketAsync(result);

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
