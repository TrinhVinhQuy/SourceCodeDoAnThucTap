using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Coffee.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Name = User.FindFirst(ClaimTypes.Name)?.Value;
                ViewBag.NameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ViewBag.GivenName = User.FindFirst(ClaimTypes.GivenName)?.Value;
                ViewBag.Surname = User.FindFirst(ClaimTypes.Surname)?.Value;
                ViewBag.Email = User.FindFirst(ClaimTypes.Email)?.Value;
            }
            return View();
        }
    }
}
