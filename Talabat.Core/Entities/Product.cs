namespace Talabat.Core.Entities
{
    public class Product : ModelBase
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; } // BrandId
        public ProductBrand ProductBrand { get; set; }

        public int CategoryId { get; set; } // CategoryId
        public ProductCategory productCategory { get; set; }
    }
}
