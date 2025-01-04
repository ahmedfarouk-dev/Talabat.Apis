namespace Talabat.Core.Entities.Order
{
    public class OrderItem : ModelBase
    {
        public OrderItem()
        {

        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
