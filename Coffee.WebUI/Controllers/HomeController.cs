using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using Coffee.DATA;

namespace Coffee.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Introduce()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Reservation()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
