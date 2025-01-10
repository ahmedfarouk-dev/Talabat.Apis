using Dashboard.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Controllers
{

    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Roles = await _roleManager.Roles.ToListAsync();

            return View(Roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            var RoleExists = await _roleManager.RoleExistsAsync(Name);

            if (RoleExists)
                return null;

            await _roleManager.CreateAsync(new IdentityRole() { Name = Name });

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(string id)
        {
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();

            await _roleManager.DeleteAsync(Role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();

            return View(Role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpViewModel model)
        {

            var Role = await _roleManager.FindByIdAsync(model.RoleId);
            if (Role is null)
                return NotFound();

            Role.Name = model.RoleName;
            await _roleManager.UpdateAsync(Role);

            return RedirectToAction("Index");
        }
    }
}
