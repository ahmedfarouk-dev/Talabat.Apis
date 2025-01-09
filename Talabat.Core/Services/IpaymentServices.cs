using Talabat.Core.Entities;

namespace Talabat.Core.Services
{
    public interface IpaymentServices
    {

        public Task<CustomerBasket?> CreateOrUpdate(string BasketId);
    }
}
