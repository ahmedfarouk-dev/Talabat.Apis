using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecCount : BaseSpecification<Product>
    {

        public ProductSpecCount(ProductSpecParems specParems) : base(

                p => (!specParems.BrandId.HasValue || p.BrandId == specParems.BrandId)
    &&
    (!specParems.CategoryId.HasValue || p.BrandId == specParems.CategoryId)
            )
        {

        }
    }
}
