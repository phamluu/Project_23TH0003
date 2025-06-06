using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XemDiem_23TH0003.Models;

namespace XemDiem_23TH0003.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //ILogger<HomeController> logger
        public HomeController()
        {
            // _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
