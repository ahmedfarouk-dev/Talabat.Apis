using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;

namespace Dashboard.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(SignInManager<UserApplication> SignInManager, UserManager<UserApplication> userManager)
        {
            _SignInManager = SignInManager;
            _UserManager = userManager;
        }

        public SignInManager<UserApplication> _SignInManager { get; }
        public UserManager<UserApplication> _UserManager { get; }

        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (model == null)
                return RedirectToAction(nameof(Index));

            var User = await _UserManager.FindByEmailAsync(model.Email);
            if (User == null)
            {
                ModelState.AddModelError("Email", "Not found");
                return RedirectToAction(nameof(Index));
            }
            var password = await _SignInManager.CheckPasswordSignInAsync(User, model.Password, false);


            if (!password.Succeeded)
            {
                ModelState.AddModelError("Password", "Not found");
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction("Index", "Home");


        }
        [HttpGet]
        public async Task<IActionResult> SinOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
