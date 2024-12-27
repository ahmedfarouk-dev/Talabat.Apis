namespace Talabat.Apis.DTOS
{
    public class ProductToReturn
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
