using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications
{
    public class OrderSpecifications : BaseSpecification<Order>
    {
        public OrderSpecifications(string Email) : base(e => e.BuyerEmail == Email)
        {
            Includes.Add(c => c.DeliveryMethod);
            Includes.Add(x => x.Items);
            OrderByAsc(x => x.OrderDate);
        }

        public OrderSpecifications(string Email, int orderId) : base(e => e.BuyerEmail == Email && orderId == e.Id)
        {
            Includes.Add(c => c.DeliveryMethod);
            Includes.Add(x => x.Items);
        }
    }
}
