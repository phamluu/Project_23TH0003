using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QLHocPhan_23TH0003.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "GiangVien")]
    public class BaseInstructorController : Controller
    {
       
    }
}
