using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ActionFilters;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProductsController : ControllerBase
    {


        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IMapper _Mapper { get; }
        public IRepositories<ProductCategory> _ProductCategory { get; }

        [HttpGet]

        public async Task<ActionResult<Pagination<Product>>> GetAll([FromQuery] ProductSpecParems specParems)
        {


            var Spec = new ProductSpecification(specParems);
            //var Products = await _product.GetAllWithSpecAsync(Spec);

            var Products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(Spec);
            var Res = _Mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturn>>(Products);

            /// var Count = await _product.GetCount(p => (!specParems.BrandId.HasValue || p.BrandId == specParems.BrandId)
            /// &&
            /// (!specParems.CategoryId.HasValue || p.BrandId == specParems.CategoryId));
            var SpecCount = new ProductSpecCount(specParems);
            var Count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(SpecCount);
            if (Products == null)
                return BadRequest(new ApiResponse(404));

            return Ok(new Pagination<ProductToReturn>(specParems.PageIndex, specParems.PageSize, Count, Res));

        }

        [HttpGet("{Id}")]
        [ValidationActionFilter]
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

            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(Spec);
            var ProductWithMapper = _Mapper.Map<Product, ProductToReturn>(product);
            if (product == null)
                return BadRequest(new ApiResponse(400));

            return Ok(ProductWithMapper);
        }

        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {

            //var AllBrands = await _productBrand.GetAllAsync();
            var AllBrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(AllBrands);
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
            return Ok(Categories);
        }
    }
}
