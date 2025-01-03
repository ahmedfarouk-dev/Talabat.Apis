using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOS
{
    public class AddressDto
    {
        [Required]
        public string Fname { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
