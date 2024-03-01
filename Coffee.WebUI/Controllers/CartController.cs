using Coffee.DATA;
using Coffee.DATA.Common;
using Coffee.DATA.Models;
using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DbCoffeeDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IHttpContextAccessor httpContextAccessor, DbCoffeeDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCart()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var CartModels = httpContext.Session.Get<List<CartModel>>("Cart") ?? new List<CartModel>();
            double totalPrice = (double)CartModels.Sum(item => item.ProductModel.Price * item.Quantity);

            var responseData = new
            {
                CartModels,
                totalPrice
            };

            return Json(responseData);
        }

        [HttpPost]
        public IActionResult AddToCart(Product product, int quantity)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var CartModels = httpContext.Session.Get<List<CartModel>>("Cart") ?? new List<CartModel>();
            var existingItem = CartModels.FirstOrDefault(item => item.ProductModel.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var _product = _dbContext.Products.Where(p => p.Id == product.Id).FirstOrDefault();
                CartModels.Add(new CartModel { ProductModel = MapToProductModel(_product), Quantity = 1 });
            }

            httpContext.Session.Set("Cart", CartModels);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var CartModels = httpContext.Session.Get<List<CartModel>>("Cart") ?? new List<CartModel>();
            var CartModelToUpdate = CartModels.FirstOrDefault(item => item.ProductModel.ProductId == productId);

            if (CartModelToUpdate != null)
            {
                CartModelToUpdate.Quantity += quantity;
                if (CartModelToUpdate.Quantity <= 0)
                {
                    CartModels.Remove(CartModelToUpdate);
                }
                httpContext.Session.Set("Cart", CartModels);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var CartModels = httpContext.Session.Get<List<CartModel>>("Cart") ?? new List<CartModel>();
            var itemToRemove = CartModels.FirstOrDefault(item => item.ProductModel.ProductId == productId);

            if (itemToRemove != null)
            {
                CartModels.Remove(itemToRemove);
                httpContext.Session.Set("Cart", CartModels);
            }
            return Json(new { success = true });
        }
        private ProductModel MapToProductModel(Product productFromDb)
        {
            return new ProductModel
            {
                ProductId = productFromDb.Id,
                Name = productFromDb.Name,
                Url = productFromDb.Image,
                Description = productFromDb.Description,
                Price = productFromDb.Price,
                categoryId = productFromDb.CategoryId,
            };
        }
    }
}
