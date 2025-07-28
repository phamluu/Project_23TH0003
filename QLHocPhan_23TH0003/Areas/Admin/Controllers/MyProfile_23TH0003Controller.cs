using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    // Quản lý cập nhật hồ sơ tài khoản của chính tôi
    public class MyProfile_23TH0003Controller : BaseAdminController
    {
        private readonly UserService _service;
        private readonly FileService _file;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public MyProfile_23TH0003Controller(UserService service, FileService fileService, 
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _service = service;
            _file = fileService;
            _userManager = userManager;
            _signInManager = signInManager;
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
