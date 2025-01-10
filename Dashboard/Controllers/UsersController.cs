using Dashboard.ViewModel;
using Dashboard.ViewModel.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;

namespace Dashboard.Controllers
{

    public class UsersController : Controller
    {
        private readonly UserManager<UserApplication> _userManager;

        public UsersController(UserManager<UserApplication> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _RoleManager = roleManager;
        }

        public RoleManager<IdentityRole> _RoleManager { get; }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UsersViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UsersViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Roles = roles
                });
            }


            return View(usersWithRoles);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var Users = await _userManager.FindByIdAsync(id);
            var AllRoles = await _RoleManager.Roles.ToListAsync();

            var ViewModle = new UserRoleViewModel()
            {
                UserId = Users.Id,
                UserName = Users.UserName,
                Roles = AllRoles.Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsSelected = _userManager.IsInRoleAsync(Users, x.Name).Result

                }).ToList()
            };
            return View(ViewModle);
        }


        [HttpPost]
        public async Task<IActionResult> Update(string id, UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
