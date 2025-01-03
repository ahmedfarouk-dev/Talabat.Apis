namespace Talabat.Apis.DTOS
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> BasketItem { get; set; }
    }
}
