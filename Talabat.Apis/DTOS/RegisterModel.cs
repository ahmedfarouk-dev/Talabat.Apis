using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOS
{
    public class RegisterModel
    {
        [Required]
        public string DisplayName { get; set; }
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}
