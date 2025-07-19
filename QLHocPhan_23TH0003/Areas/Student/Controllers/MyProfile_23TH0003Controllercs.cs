using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class MyProfile_23TH0003Controller : BaseStudentController
    {
        private readonly UserService _service;
        public MyProfile_23TH0003Controller(UserService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _service.GetUserInfo(UserId);
            return View(model);
        }
        public ActionResult EditProfileSinhVien()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _service.GetSinhVienInfo(UserId);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfileSinhVien(SinhVien model, IFormFile? HinhDaiDienFile)
        {
            if (ModelState.IsValid)
            {
                bool result = await _service.UpdateSinhVien(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật hồ sơ sinh viên thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Cập nhật hồ sơ sinh viên không thành công";
                }
            }
            TempData["ErrorMessage"] = "Cập nhật hồ sơ sinh viên không thành công";
            return View(model);
        }
    }
}
