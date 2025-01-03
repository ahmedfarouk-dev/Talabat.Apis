using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<UserApplication> _userManager;

        public AuthServices(UserManager<UserApplication> manager, IOptions<Jwt> jwt)
        {
            _userManager = manager;
            _Jwt = jwt;
        }

        public IOptions<Jwt> _Jwt { get; }

        public async Task<JwtSecurityToken> CreateTokenAsync(UserApplication user)
        {


            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.DisplayName),
                new Claim("UserId", user.Id)
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
             issuer: _Jwt.Value.Issuer,
             audience: _Jwt.Value.Audience,
             claims: claims,
             expires: DateTime.Now.AddDays(_Jwt.Value.ExpiryMinutes),
             signingCredentials: signingCredentials);

            return jwtSecurityToken;

        }
    }
}
