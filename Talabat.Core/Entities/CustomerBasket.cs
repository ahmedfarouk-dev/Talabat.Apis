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
    }
}
