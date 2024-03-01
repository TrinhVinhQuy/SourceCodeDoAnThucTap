using Coffee.DATA;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Coffee.WebUI.Models;
using Coffee.DATA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;

namespace Coffee.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbCoffeeDbContext _dbCoffeeDbContext;
        public LoginController(DbCoffeeDbContext dbCoffeeDbContext)
        {
            _dbCoffeeDbContext = dbCoffeeDbContext;
        }
        //[Route("/login")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginSave(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = md5.ComputeMD5Hash(model.Password);
                var user = await _dbCoffeeDbContext.Users.FirstOrDefaultAsync(x => x.UserName.Contains(model.UserName) && x.Password.Contains(hashedPassword) && x.Status == true);
                if (user != null)
                {
                    var role = await _dbCoffeeDbContext.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Name),
                        new Claim(ClaimTypes.Role, role.Name)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";

                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
