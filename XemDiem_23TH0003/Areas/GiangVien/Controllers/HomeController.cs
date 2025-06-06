using Microsoft.AspNetCore.Mvc;

namespace XemDiem_23TH0003.Areas.GiangVien.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
