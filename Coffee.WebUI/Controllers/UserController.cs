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

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Name = User.FindFirst(ClaimTypes.Name)?.Value;
                ViewBag.NameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ViewBag.GivenName = User.FindFirst(ClaimTypes.GivenName)?.Value;
                ViewBag.Surname = User.FindFirst(ClaimTypes.Surname)?.Value;
                ViewBag.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                ViewBag.Role = User.FindFirst(ClaimTypes.Role)?.Value;
            }
            return View();
        }
        //public Task SendOTP()
        //{
        //    var accountSid = "AC6fd617572dc4c4f1731102da9aa95a11";
        //    var authToken = "6e898044665551af057f99f90384c102";
        //    TwilioClient.Init(accountSid, authToken);

        //    return MessageResource.CreateAsync(
        //      to: new PhoneNumber("+84946453657"),
        //      from: new PhoneNumber("12202723681"),
        //      body: "180729");
        //}

    }
}
