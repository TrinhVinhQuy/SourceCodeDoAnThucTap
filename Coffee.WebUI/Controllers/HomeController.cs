using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using Coffee.DATA;
using Coffee.DATA.Models;
using System.Net.Mail;

namespace Coffee.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbCoffeeDbContext _db;

        public HomeController(ILogger<HomeController> logger, DbCoffeeDbContext db)
        {
            _logger = logger;
            _db = db;
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
        [HttpPost]
        public IActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                var _contact = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedOn = DateTime.Now,
                    Status = true,
                };
                _db.Contact.Add(_contact);
                _db.SaveChanges();
                ViewBag.Success = "Cảm ơn quý khách đã gữi liên hệ!";
            }
            return View();
        }
        public IActionResult Reservation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reservation(Book model)
        {
            if (ModelState.IsValid)
            {
                var _book = new Book
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Day = model.Day,
                    Time = model.Time,
                    Seates = model.Seates,
                    Status = false,
                };
                _db.Book.Add(_book);
                _db.SaveChanges();
                ViewBag.Success = "Cảm ơn quý khách đã đặt bàn. Vui lòng check lại email để lấy thông tin đặt bàn!";
                // Mật khẩu ứng dụng OtpEmail : kemz hkfu jode ctfp
                Random random = new Random();
                var randomNumber = random.Next(100000, 1000000);
                MailMessage message = new MailMessage("txvq0101@gmail.com", model.Email, "Thông tin đặt bàn", "Cảm ơn quý khách đã đặt bàn chúng tôi sẽ liên hệ lại sau!");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("txvq0101@gmail.com", "kemz hkfu jode ctfp");
                client.Send(message);
            }
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
