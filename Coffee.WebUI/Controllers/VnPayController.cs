using Coffee.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using log4net;
using System;
using System.Threading.Tasks;

namespace Coffee.WebUI.Controllers
{
    public class VnPayController : Controller
    {
        private readonly IConfiguration _configuration;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VnPayController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Pay()
        {
            //try
            //{
            string vnp_Returnurl = _configuration["Vnpay:vnp_Returnurl"];
            string vnp_Url = _configuration["Vnpay:vnp_Url"];
            string vnp_TmnCode = _configuration["Vnpay:vnp_TmnCode"];
            string vnp_HashSecret = _configuration["Vnpay:vnp_HashSecret"];

            OrderInfo order = new OrderInfo
            {
                OrderId = DateTime.Now.Ticks,
                Amount = 100000,
                Status = "0",
                CreatedDate = DateTime.Now
            };

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(HttpContext));
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
            vnpay.AddRequestData("vnp_Locale", "vn");

            string paymentUrl = await Task.Run(() => vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret)); // Thực hiện tạo URL thanh toán bất đồng bộ

            log.InfoFormat("VNPAY URL: {0}", paymentUrl);

            return Redirect(paymentUrl);
            //}
            //catch (Exception ex)
            //{
            //    log.ErrorFormat("Error initiating payment: {0}", ex.Message);
            //    return BadRequest($"Error initiating payment: {ex.Message}");
            //}
        }
    }
}
