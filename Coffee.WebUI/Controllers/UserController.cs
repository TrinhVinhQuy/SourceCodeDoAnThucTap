using Coffee.DATA.Models;
using Coffee.DATA.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;

namespace Coffee.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var _user = await _userRepository.GetAllAsync();
            var _userDetail = _user.First(x => x.Email == User.FindFirst(ClaimTypes.Email)?.Value);
            return View(_userDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Index(User model)
        {
            return Json(model);
        }
    }
}
