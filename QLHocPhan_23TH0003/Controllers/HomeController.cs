using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Models;
using System.Diagnostics;

namespace QLHocPhan_23TH0003.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Kiểm tra nếu chưa có người dùng nào sẽ về trang đăng ký tài khoản set quyền admin
            var userCount = _userManager.Users.Count();
            if (userCount == 0)
            {
                return Redirect("/Identity/Account/Register");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
