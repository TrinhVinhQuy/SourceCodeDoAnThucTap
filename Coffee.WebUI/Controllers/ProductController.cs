using Coffee.DATA;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbCoffeeDbContext _dbCoffeeDbContext;
        public ProductController(DbCoffeeDbContext dbCoffeeDbContext)
        {
            _dbCoffeeDbContext = dbCoffeeDbContext;
        }

        public IActionResult Index(string url)
        {
            return View();
        }
        public IActionResult ProductDetails(string url)
        {
            ViewBag.Name = "Chê";
            return View();
        }
    }
}
