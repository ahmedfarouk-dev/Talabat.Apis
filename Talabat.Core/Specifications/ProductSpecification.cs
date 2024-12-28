using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification(ProductSpecParems specParems) : base(

            p => (!specParems.BrandId.HasValue || p.BrandId == specParems.BrandId)


            &&


            (!specParems.CategoryId.HasValue || p.BrandId == specParems.CategoryId))
        {

            CommonIncludes();
            if (!string.IsNullOrWhiteSpace(specParems.Sort))
            {
                switch (specParems.Sort)
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
            // pagesize = 5;  //// 4
            // pageindex =2;
            // count =20;
            ApplyPagination((specParems.PageIndex - 1) * (specParems.PageSize), specParems.PageSize);

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
