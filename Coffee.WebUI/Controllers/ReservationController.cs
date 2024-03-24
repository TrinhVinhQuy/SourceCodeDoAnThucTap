using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
