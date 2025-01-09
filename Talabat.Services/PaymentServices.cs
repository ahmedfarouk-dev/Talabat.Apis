using Microsoft.Extensions.Configuration;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentServices : IpaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            this._configuration = configuration;
            _BasketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
        }

        public IBasketRepository _BasketRepository { get; }

        public async Task<CustomerBasket?> CreateOrUpdate(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["stripe:Secretkey"];

            var basket = await _BasketRepository.GetBasketAsync(BasketId);

            if (basket == null)
                return null;
            var ShippingCost = 0m;
            if (basket.DeliveryMethod.HasValue)
            {
                var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethod.Value);

                ShippingCost = DeliveryMethod.Cost;

            }

            if (basket.BasketItem.Count > 0)
            {
                foreach (var item in basket.BasketItem)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (product.Price != item.Price)
                    {
                        item.Price = product.Price;
                    }

                }


            }


            var SubTotal = basket.BasketItem.Sum(x => x.Price * x.Quantity);

            var Service = new PaymentIntentService();
            var PaymentIntent = new PaymentIntent();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {

                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingCost * 100,

                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }

                };
                PaymentIntent = await Service.CreateAsync(option);

                basket.PaymentIntentId = PaymentIntent.Id;
                basket.Secretkey = PaymentIntent.ClientSecret;

            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)SubTotal * 100 + (long)ShippingCost * 100,
                };
                PaymentIntent = await Service.UpdateAsync(basket.PaymentIntentId, option);

                basket.PaymentIntentId += PaymentIntent.Id;
                basket.Secretkey = PaymentIntent?.ClientSecret;

                _BasketRepository.UpdateBasketAsync(basket);



            }


            return basket;
        }
    }
}
