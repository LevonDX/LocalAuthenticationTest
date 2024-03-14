using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalAuthenticationTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ProductController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            string? username = _userManager.GetUserName(User);

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            await _userManager.AddToRoleAsync((await _userManager.FindByNameAsync(username!))!, "Admin");

            return View(model: $"Hello from products!!!, dear {username}");
        }

        public IActionResult Details()
        {
            return View(model: "Hello from prodcuct details");
        }
    }
}
