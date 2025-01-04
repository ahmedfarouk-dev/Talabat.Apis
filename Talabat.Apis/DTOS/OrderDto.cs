using Talabat.Core.Entities.Order;

namespace Talabat.Apis.DTOS
{
    public class OrderDto
    {
        //string BuyerEmail, string BasketId, int DeliveryMethodId, Addres ShippingAddress

        public string BuyerEmail { get; set; }

        public string BasketId { get; set; }

        public int DeliveryMethodId { get; set; }
        public Addres ShippingAddress { get; set; }
    }
}
