using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOS
{
    public class BasketItemDto
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The PictureUrl field is required.")]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "The Brand field is required.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The Type field is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Price must be at least 1.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "The Quantity must be at least 0.1.")]
        public decimal Quantity { get; set; }

    }
}
