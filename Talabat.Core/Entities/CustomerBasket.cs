namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {


        public string Id { get; set; }

        public List<BasketItem> BasketItem { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }


        public string? PaymentIntentId { get; set; }
        public string? Secretkey { get; set; }
        public int? DeliveryMethod { get; set; }
    }
}
