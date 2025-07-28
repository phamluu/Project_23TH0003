using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Areas.Admin.Controllers;
using QLHocPhan_23TH0003.Areas.Instructor.Controllers;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Controllers
{
    public class MyProfile_23TH0003Controller : BaseInstructorController
    {
        private readonly UserService _service;
        private readonly FileService _file;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public MyProfile_23TH0003Controller(UserService service, FileService fileService, 
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _file = fileService;
            _signInManager = signInManager;
            _userManager = userManager;
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

        #region Quản lý tài khoản
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy người dùng.");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordMismatch")
                    {
                        ModelState.AddModelError(nameof(model.CurrentPassword), "Mật khẩu hiện tại không đúng.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description); // hoặc tự dịch các lỗi khác nếu cần
                    }
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            ViewBag.StatusMessage = "✅ Mật khẩu đã được thay đổi thành công.";
            ModelState.Clear(); // Xóa lỗi cũ nếu có
            return View(new ChangePasswordViewModel()); // Trả form trống
        }

        #endregion
    }
}
