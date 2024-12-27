using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
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

        public ProductsController(IRepositories<Product> Product, IMapper mapper)
        {
            _product = Product;
            _Mapper = mapper;
        }

        public IMapper _Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var Spec = new ProductSpecification();
            var Products = await _product.GetAllWithSpecAsync(Spec);
            if (Products == null)
                return BadRequest(new ApiResponse(404));
            return Ok(Products);

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturn>> GetById(int Id)
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
            var ProductWithMapper = _Mapper.Map<Product, ProductToReturn>(product);
            if (product == null)
                return BadRequest(new ApiResponse(400));

            return Ok(ProductWithMapper);
        }


        [HttpGet("Server/{id}")]
        public async Task<IActionResult> Server(int id)
        {
            var Spec = new ProductSpecification(id);

            var product = await _product.GetByIdWithSpecAsync(Spec);
            product.PictureUrl.ToString();
            return Ok(product);
        }
    }
}
