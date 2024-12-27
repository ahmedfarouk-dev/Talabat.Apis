using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification() : base()
        {
            CommonIncludes();
        }
        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            CommonIncludes();
        }

        public void CommonIncludes()
        {

            Includes.Add(p => p.productCategory);
            Includes.Add(p => p.ProductBrand);
        }
    }
}
