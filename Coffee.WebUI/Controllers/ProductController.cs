using Coffee.DATA;
using Coffee.DATA.Models;
using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coffee.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbCoffeeDbContext _db;
        public ProductController(DbCoffeeDbContext dbCoffeeDbContext)
        {
            _db = dbCoffeeDbContext;
        }
        public IActionResult Index(string? query, string? search, int? searchCategory)
        {
            var productsQuery = _db.Products
                .Include(p => p.ProductImages)
                .Select(p => new ProductModel
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Price = (decimal)p.Price,
                    Description = p.Description,
                    Image = p.Image,
                    Url = p.Url,
                    categoryName = _db.Categories.FirstOrDefault(x => x.Id == p.CategoryId).Name,
                    categoryId = p.CategoryId,
                    ProductImages = p.ProductImages.Select(pi => new ProductImageModel
                    {
                        ProductImageId = pi.Id,
                        ImageUrl = pi.UrlImage,
                    }).ToList()
                });

            if (query != null && query == "asc")
            {
                productsQuery = productsQuery.OrderBy(x => x.Price);
            }
            if (query != null && query == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(x => x.Price);
            }
            if (query != null && query == "name")
            {
                productsQuery = productsQuery.OrderBy(x => x.Name);
            }
            if (search != null)
            {
                productsQuery = productsQuery.Where(x => x.Name.Contains(search));
            }
            if (searchCategory != null)
            {
                productsQuery = productsQuery.Where(x => x.categoryId == searchCategory);
            }
            var products = productsQuery.ToList();

            ViewBag.Category = _db.Categories.Include(c => c.Products).ToList();
            //ViewBag.TopProduct = 
            return View(products);
        }



        public IActionResult ProductDetails(string url)
        {
            ViewBag.Name = "Chê";
            return View();
        }
    }
}
