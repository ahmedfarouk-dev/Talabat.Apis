using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IBasketRepository
    {


        Task<CustomerBasket?> GetBasketAsync(string BasketId);

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);

        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
