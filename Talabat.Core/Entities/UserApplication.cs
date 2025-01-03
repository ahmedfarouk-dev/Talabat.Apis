using Microsoft.AspNetCore.Identity;

namespace Talabat.Core.Entities
{
    public class UserApplication : IdentityUser
    {

        public string DisplayName { get; set; }

        public Address Address { get; set; }
    }
}
