using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {

        public Task<Order> CreateOrder(String BuyerEmail, String BasketId, int DeliveryMethodId, Addres ShippingAddress);


    }
}
