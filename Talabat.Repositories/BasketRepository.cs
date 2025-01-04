using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _Database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _Database = connection.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _Database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await _Database.StringGetAsync(BasketId);
            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);

            var CreateOrUpdate = await _Database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(30));

            if (!CreateOrUpdate) return null;

            return await GetBasketAsync(Basket.Id);

        }
    }
}
