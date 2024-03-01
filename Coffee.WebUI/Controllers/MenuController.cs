using Coffee.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using Coffee.WebUI.Models;

namespace Coffee.WebUI.Controllers
{
    public class MenuController : Controller
    {
        private readonly DbCoffeeDbContext _dbCoffeeDbContext;
        public MenuController(DbCoffeeDbContext dbCoffeeDbContext)
        {
            _dbCoffeeDbContext = dbCoffeeDbContext;
        }

        public IActionResult Index()
        {
            ViewBag.ListCategory = _dbCoffeeDbContext.Categories.ToList();
            var products = _dbCoffeeDbContext.Products
                .Include(p => p.ProductImages)
                .Select(p => new ProductModel
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Price = (decimal)p.Price,
                    Description = p.Description,
                    Url = p.Url,
                    categoryId = p.CategoryId,
                    ProductImages = p.ProductImages.Select(pi => new ProductImageModel
                    {
                        ProductImageId = pi.Id,
                        ImageUrl = pi.UrlImage,
                    }).ToList()
                })
                .ToList();
            return View(products);
        }
    }
}
