using Coffee.DATA;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Coffee.WebUI.Models;
using Coffee.DATA.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using System.Net.Mail;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using Coffee.DATA.Models;
using System.Net.Http;
using Coffee.DATA.Repository;

namespace Coffee.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbCoffeeDbContext _dbCoffeeDbContext;
        private readonly IRepository<User> _userRepository;
        public LoginController(DbCoffeeDbContext dbCoffeeDbContext, IRepository<User> userRepository)
        {
            _dbCoffeeDbContext = dbCoffeeDbContext;
            _userRepository = userRepository;
        }
        //[Route("/login")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginSave(LoginModel model)
        {
            // Quy@0104
            if (ModelState.IsValid)
            {
                var hashedPassword = md5.ComputeMD5Hash(model.Password);
                var user = await _dbCoffeeDbContext.Users.FirstOrDefaultAsync(x => x.Email.Contains(model.Email) && x.Password.Contains(hashedPassword) && x.Status == true);
                if (user != null)
                {
                    var role = await _dbCoffeeDbContext.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, role.Name)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";

                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Login")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var user = result.Principal;
                var emailClaim = user.FindFirst(ClaimTypes.Email).Value;
                var checkEmail = _dbCoffeeDbContext.Users.Where(x => x.Email == emailClaim);
                if (checkEmail.Count() < 1)
                {
                    var newUser = new User { Email = emailClaim, RoleId = 2, Status = true, CreatedOn = DateTime.Now };
                    _dbCoffeeDbContext.Users.Add(newUser);
                    _dbCoffeeDbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/send-otp")]
        public async Task<IActionResult> SendOTPEmail(string email)
        {
            var checkEmail = await _userRepository.GetAllAsync();

            if (checkEmail.Where(x => x.Email == email).Count() > 0)
            {
                return Json(new { success = false, message = "Email đã tồn tại!" });
            }
            else
            {
                // Mật khẩu ứng dụng OtpEmail : kemz hkfu jode ctfp
                Random random = new Random();
                var randomNumber = random.Next(100000, 1000000);
                MailMessage message = new MailMessage("txvq0101@gmail.com", email, "Otp", Convert.ToString(randomNumber));
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("txvq0101@gmail.com", "kemz hkfu jode ctfp");
                client.Send(message);
                HttpContext.Session.SetString("OTP", Convert.ToString(randomNumber));
                //HttpContext.Session.SetString("OTP", "1");
                return Json(new { success = true, message = "Vui lòng xem email để lấy mã OTP!" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string otp, string name)
        {
            var otpss = HttpContext.Session.GetString("OTP");
            if (otpss == otp)
            {
                var _user = new User { Email = email, Password = md5.ComputeMD5Hash(password), Status = true, CreatedOn = DateTime.Now, RoleId = 2, Name = name };
                try
                {
                    await _userRepository.InsertAsync(_user);
                    return Json(new { success = true, message = "Đăng kí thành công!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Fail: " + ex });
                }
            }
            else
            {
                return Json(new { success = false, message = "Mã OTP không khớp" });
            }
        }
    }
}
