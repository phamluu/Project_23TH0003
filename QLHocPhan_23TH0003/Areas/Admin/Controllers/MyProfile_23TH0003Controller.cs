using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class MyProfile_23TH0003Controller : BaseAdminController
    {
        private readonly UserService _service;
        private readonly FileService _file;
        public MyProfile_23TH0003Controller(UserService service, FileService fileService)
        {
            _service = service;
            _file = fileService;
        }
        // Show thông tin tài khoản
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
        public async Task<ActionResult> EditProfileGiangVien(GiangVien model, IFormFile HinhDaiDienFile)
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
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;

                    foreach (var error in errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"ModelState Error - {key}: {error.ErrorMessage}");
                    }
                }
            }
            TempData["ErrorMessage"] = "Cập nhật hồ sơ giảng viên không thành công";
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
        public async Task<ActionResult> EditProfileSinhVien(SinhVien model)
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
