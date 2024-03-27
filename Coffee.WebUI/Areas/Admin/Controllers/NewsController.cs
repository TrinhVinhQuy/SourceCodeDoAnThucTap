using Coffee.DATA.Common;
using Coffee.DATA.Models;
using Coffee.DATA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Coffee.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly IRepository<New> _newRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IWebHostEnvironment _environment;
        public NewsController(IRepository<New> newRepository, IWebHostEnvironment environment, IRepository<User> userRepository)
        {
            _newRepository = newRepository;
            _environment = environment;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(New model, IFormFile mainImage)
        {
            try
            {
                var helper = new Helper();
                var urlImg = "/" + await SaveImage(mainImage);
                var _user = await _userRepository.GetAllAsync();
                var _new = new New
                {
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    Keywords = model.Keywords,
                    Image = urlImg,
                    Url = helper.ConvertToSlug(model.Title),
                    UserId = _user.First(x => x.Email == User.FindFirst(ClaimTypes.Email)?.Value).Id,
                    Status = false,
                    CreatedOn = DateTime.Now,
                };
                await _newRepository.InsertAsync(_new);
                return Json(new { success = true, message = "Thêm bài viết thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Fail: " + ex });
            }
        }
        public async Task<IActionResult> GetAllNews()
        {
            //var 
            return Json(new { success = true });
        }
        public async Task<string> SaveImage(IFormFile image)
        {
            // Tạo tên tệp duy nhất cho hình ảnh
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_environment.WebRootPath, "assets", "news", fileName);

            // Lưu hình ảnh vào thư mục wwwroot/assets/product
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối của hình ảnh
            return Path.Combine("assets", "news", fileName).Replace("\\", "/");
        }
    }
}
