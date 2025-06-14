using Microsoft.AspNetCore.Mvc;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class HomeController : BaseStudentController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
