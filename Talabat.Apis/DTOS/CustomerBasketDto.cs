namespace Talabat.Apis.DTOS
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> BasketItem { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? Secretkey { get; set; }
        public int? DeliveryMethod { get; set; }
    }
}
