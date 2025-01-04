using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IBasketRepository basket, IUnitOfWork unitOfWork)
        {
            _basket = basket;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrder(string BuyerEmail, string BasketId, int DeliveryMethodId, Addres ShippingAddress)
        {
            // 1.Get Basket From Basket Repo

            var Basket = await _basket.GetBasketAsync(BasketId);


            //2.Get Selected Items at Basket From Product Repo

            var OrderItem = new List<OrderItem>();
            if (Basket?.BasketItem?.Count > 0)
            {
                foreach (var item in Basket.BasketItem)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    var ProductItem = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                    var OrdreItem = new OrderItem(ProductItem, product.Price, item.Quantity);

                    OrderItem.Add(OrdreItem);
                }
            }


            //3.Calculate SubTotal

            var SubTotal = OrderItem.Sum(s => s.Price * s.Quantity);

            //4.Get Delivery Method From DeliveryMethod Repo

            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            //5.Create Order

            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItem, SubTotal);

            //6.Add Order Locally

            _unitOfWork.Repository<Order>().Add(Order);

            //7.Save Order To Database[ToDo]
            var Result = await _unitOfWork.Complete();


            return Order;

        }

        public Task<Order> GetOrderByIdForSpecificUser(string email, int OrderId)
        {
            var Spec = new OrderSpecifications(email, OrderId);

            var orders = _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(Spec);

            return orders;

        }

        public Task<IEnumerable<Order>> GetOrdersForSpecificUser(string BuyerEmail)
        {

            var Spec = new OrderSpecifications(BuyerEmail);

            var orders = _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);

            return orders;

        }




    }
}
