using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications
{
    public class OrderPaymentSpecifications : BaseSpecification<Order>
    {
        public OrderPaymentSpecifications(string PaymentIntentId) : base(x => x.PaymentIntentId == PaymentIntentId)
        {

        }
    }
}
