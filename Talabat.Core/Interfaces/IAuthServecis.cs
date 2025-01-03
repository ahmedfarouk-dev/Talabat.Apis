using System.IdentityModel.Tokens.Jwt;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces
{
    public interface IAuthServices
    {

        public Task<JwtSecurityToken> CreateTokenAsync(UserApplication user);

    }
}
