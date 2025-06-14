using Microsoft.AspNetCore.Mvc;

namespace QLHocPhan_23TH0003.Areas.Instructor.Controllers
{
    public class HomeController : BaseInstructorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
