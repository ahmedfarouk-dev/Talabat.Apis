using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification() : base()
        {
            Includes.Add(p => p.productCategory);
            Includes.Add(p => p.ProductBrand);
        }
        public ProductSpecification(int id) : base(p => p.Id == id)
        {

        }
    }
}
