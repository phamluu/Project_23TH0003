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
            try
            {
                var hasUser = _userManager.Users.Any();
                if (!hasUser)
                {
                    return Redirect("/Identity/Account/Register");
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra trong HomeController.Index khi đếm số lượng người dùng.");
                return RedirectToAction("Error");
            }
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
