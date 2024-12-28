using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification(string? sort, int? BrandId, int? CategoryId) : base(

            p => (!BrandId.HasValue || p.BrandId == BrandId)


            &&


            (!CategoryId.HasValue || p.BrandId == CategoryId))
        {

            CommonIncludes();
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        this.OrderByAsc(p => p.Price);
                        break;
                    case "priceDsc":
                        this.OrderByDsc(p => p.Price);

                        break;
                    default:
                        this.OrderByAsc(n => n.Name);
                        break;
                }
            }
            else
                OrderByAsc(p => p.Name);
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
