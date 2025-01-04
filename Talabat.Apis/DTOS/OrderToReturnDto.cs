using Talabat.Core.Entities.Order;

namespace Talabat.Apis.DTOS
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Addres ShippingAddress { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public string DeliveryMethodShortName { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
         => SubTotal + DeliveryMethodCost;
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
