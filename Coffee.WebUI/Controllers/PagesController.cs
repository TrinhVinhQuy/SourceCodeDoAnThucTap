using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebUI.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("bai-viet")]
        public IActionResult Blog()
        {
            return View();
        }
        [Route("nhan-vien")]
        public IActionResult Staff()
        {
            return View();
        }
        [Route("faq")]
        public IActionResult FAQ()
        {
            return View();
        }
        [Route("cau-chuyen")]
        public IActionResult Story()
        {
            return View();
        }
        [Route("trung-bay")]
        public IActionResult Gallery()
        {
            return View();
        }
    }
}
