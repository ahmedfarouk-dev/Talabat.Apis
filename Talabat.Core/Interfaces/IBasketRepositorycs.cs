using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IBasketRepositorycs
    {


        Task<CustomerBasket?> GetBasketAsync(string BasketId);

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);

        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
