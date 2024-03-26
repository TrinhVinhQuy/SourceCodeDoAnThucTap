using Coffee.DATA;
using Coffee.DATA.Models;
using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Coffee.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbCoffeeDbContext _db;
        public ProductController(DbCoffeeDbContext dbCoffeeDbContext)
        {
            _db = dbCoffeeDbContext;
        }
        public IActionResult Index(string? query, string? search, int? searchCategory, int? page, string? searchString)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
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
            else if (query != null && query == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(x => x.Price);
            }
            else if (query != null && query == "name")
            {
                productsQuery = productsQuery.OrderBy(x => x.Name);
            }

            if (!string.IsNullOrEmpty(search))
            {
                productsQuery = productsQuery.Where(x => x.Name.Contains(search));
            }

            if (searchCategory != null)
            {
                productsQuery = productsQuery.Where(x => x.categoryId == searchCategory);
            }

            var products = productsQuery.AsQueryable().ToPagedList(pageNumber, pageSize);

            ViewBag.Category = _db.Categories.Include(c => c.Products).ToList();
            ViewBag.CountProducts = _db.Products.Count();
            ViewBag.TopProducts = _db.Products.OrderByDescending(x=>x.Id).Take(4).ToList();
            return View(products);
        }

        public IActionResult ProductDetails(string url)
        {
            ViewBag.Name = "Chê";
            return View();
        }
    }
}
