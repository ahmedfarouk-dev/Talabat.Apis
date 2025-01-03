using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities;

namespace Talabat.Apis.Extension
{
    public static class UserManagerExtension
    {

        public static async Task<UserApplication> GetUserByAddress(this UserManager<UserApplication> _UserManager, ClaimsPrincipal US)
        {
            var Email = US.FindFirstValue(ClaimTypes.Email);

            var User = _UserManager.Users.Include(a => a.Address).FirstOrDefault(e => e.NormalizedEmail == Email.ToUpper());
            return User;
        }
    }
}
