


using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {

        public Task<Order> CreateOrder(String BuyerEmail, String BasketId, int DeliveryMethodId, Addres ShippingAddress);


        public Task<IEnumerable<Order>> GetOrdersForSpecificUser(string BuyerEmail);
        public Task<Order> GetOrderByIdForSpecificUser(string email, int OrderId);
    }
}
