using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositories<Product> _product;

        public ProductsController(IRepositories<Product> Product)
        {
            _product = Product;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var Products = await _product.GetAllAsync();
            if (Products == null)
                return BadRequest();
            return Ok(Products);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetById(int Id)
        {

            var product = await _product.GetByIdAsync(Id);
            if (product == null)
                return BadRequest();

            return Ok(product);
        }
    }
}
