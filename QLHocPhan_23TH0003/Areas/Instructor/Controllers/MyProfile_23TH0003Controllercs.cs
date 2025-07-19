using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Areas.Admin.Controllers;
using QLHocPhan_23TH0003.Areas.Instructor.Controllers;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Controllers
{
    public class MyProfile_23TH0003Controller : BaseInstructorController
    {
        private readonly UserService _service;
        private readonly FileService _file;
        public MyProfile_23TH0003Controller(UserService service, FileService fileService)
        {
            _service = service;
            _file = fileService;
        }
        public IActionResult Index()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _service.GetUserInfo(UserId);
            return View(model);
        }

        public ActionResult EditProfileGiangVien()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gianVien = _service.GetGiangVienInfo(UserId);
            if (gianVien == null)
            {
                return NotFound();
            }

            return View(gianVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfileGiangVien(GiangVien model, IFormFile? HinhDaiDienFile)
        {
            if (ModelState.IsValid)
            {
                if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
                {
                    string fileName = await _file.UploadAndGetResultStringAsync(HinhDaiDienFile);
                    model.HinhDaiDien = fileName;
                }
                bool result = await _service.UpdateGiangVien(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cập nhật hồ sơ giảng viên thành công";
                    return RedirectToAction("EditProfileGiangVien");
                }
                else
                {
                    TempData["ErrorMessage"] = "Cập nhật hồ sơ giảng viên không thành công";
                }
            }
            else
            {
                string rs = "";
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;

                    foreach (var error in errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"ModelState Error - {key}: {error.ErrorMessage}");
                        rs += error.ErrorMessage;
                    }
                }
                TempData["ErrorMessage"] = "Cập nhật hồ sơ giảng viên không thành công. " + rs;
            }
            
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
    }
}
