using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;

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
            var Spec = new ProductSpecification();
            var Products = await _product.GetAllWithSpecAsync(Spec);
            if (Products == null)
                return BadRequest();
            return Ok(Products);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetById(int Id)
        {

            ///var Spec = new BaseSpecification<Product>()
            ///{
            ///    Criteria = p => p.Id == Id,
            ///    Includes = new List<System.Linq.Expressions.Expression<Func<Product, object>>>()
            ///    {
            ///        p =>p.ProductBrand,
            ///        p =>p.productCategory
            ///    }
            ///
            ///};
            ///

            var Spec = new ProductSpecification(Id);

            var product = await _product.GetByIdWithSpecAsync(Spec);
            if (product == null)
                return BadRequest();

            return Ok(product);
        }
    }
}
