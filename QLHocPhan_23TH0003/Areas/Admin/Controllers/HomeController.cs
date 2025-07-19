using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
   
    public class HomeController : BaseAdminController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly QuanLyHocPhanDbContext _context;
        private readonly UserService _service;
        public HomeController(SignInManager<IdentityUser> signInManager, QuanLyHocPhanDbContext context,
            UserService service)
        {
            _signInManager = signInManager;
            _context = context;
            _service = service;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // Phải có tài khoản đăng nhập mới tự xem được hồ sơ người dùng
        public ActionResult Profile(string id)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _service.GetUserInfo(UserId);
            return View(model);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();  // Đăng xuất người dùng

            if (returnUrl != null)
            {
                return Redirect(returnUrl);  // Chuyển hướng nếu có URL
            }

            return RedirectToAction("Index", "Home", new { area = "" });  // Hoặc quay về trang chủ nếu không có returnUrl
        }
    }
}
