using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Talabat.Apis.DTOS;
using Talabat.Apis.Extension;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;

namespace Talabat.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> _UserManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<UserApplication> user, IAuthServices authServices, IMapper mapper)
        {
            _UserManager = user;
            _AuthServices = authServices;
            _mapper = mapper;
        }

        public IAuthServices _AuthServices { get; }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _UserManager.FindByEmailAsync(model.Email) is not null)
                return new AuthResponse { Message = "Email is already registered!" };

            if (await _UserManager.FindByNameAsync(model.Username) is not null)
                return new AuthResponse { Message = "Username is already registered!" };


            var User = new UserApplication()
            {
                UserName = model.Username,
                Email = model.Email,
                DisplayName = model.DisplayName,
            };

            var Result = await _UserManager.CreateAsync(User, model.Password);
            if (Result.Succeeded)

                if (!Result.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in Result.Errors)
                        errors += $"{error.Description},";
                    return new AuthResponse { Message = errors };
                }
            var jwtSecurityToken = await _AuthServices.CreateTokenAsync(User);

            return new AuthResponse
            {
                Email = User.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = User.UserName
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginModel model)
        {
            var authModel = new AuthResponse();
            var user = await _UserManager.FindByEmailAsync(model.Email);
            if (user is null || !await _UserManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            var Token = await _AuthServices.CreateTokenAsync(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = Token.ValidTo;
            return Ok(authModel);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CurrentUser>> GetCurrentUser()
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _UserManager.FindByEmailAsync(Email);
            var jwtSecurityToken = await _AuthServices.CreateTokenAsync(user);
            var CurrentUser = new CurrentUser()
            {
                Email = Email,
                Id = user.Id,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

            return Ok(CurrentUser);
        }

        [HttpGet("GetUserAdderss")]
        [Authorize]
        public async Task<ActionResult<Address>> GetUserAdderss()
        {


            var user = await _UserManager.GetUserByAddress(User);

            return user.Address;
        }


        [HttpPost("UpdateUserAddress")]
        [Authorize]
        public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto addressDto)
        {

            var user = await _UserManager.GetUserByAddress(User);
            var address = _mapper.Map<Address>(addressDto);
            if (user == null)
                return NotFound(404);


            if (user.Address is null)
            {
                user.Address = address;
            }
            else
            {

                address.Id = user.Address.Id;
                user.Address = address;
            }

            var Result = await _UserManager.UpdateAsync(user);

            if (!Result.Succeeded)
                return BadRequest(Result);



            return Ok(address);
        }

        [HttpGet("EmailIsExists")]
        public async Task<bool> EmailIsExists(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email) is not null;
        }
    }
}
