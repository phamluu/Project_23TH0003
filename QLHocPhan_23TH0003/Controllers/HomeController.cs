using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;
using System.Diagnostics;

namespace QLHocPhan_23TH0003.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MainDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager,
            MainDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            try
            {
                if (!_userManager.Users.Any())
                    return Redirect("/Identity/Account/Register");
                string IdUser = _userManager.GetUserId(User);
                var sinhVien = _context.SinhVien.FirstOrDefault(sv => sv.UserId == IdUser);
                int? IdSinhVien = sinhVien == null ? null : sinhVien.Id;

                if (!_context.HocKy.Any())
                {
                    return View();
                }
                var hocKys = _context.HocKy
                                .AsNoTracking()
                                .Where(hk => hk.HocPhans.Any(hp =>
                                    !hp.IsDeleted &&
                                    hp.LopHocPhans.Any(lhp => !lhp.IsDeleted)))
                                .OrderByDescending(hk => hk.ThuTu)
                                .ToList();

                if (!hocKys.Any())
                    return View(new List<HocKyLopHocPhanViewModel>());

                // 2. Lấy danh sách khoa
                var khoas = _context.Khoa
                    .AsNoTracking()
                    .ToList();

                // 3. Lấy lớp học phần và các quan hệ cần thiết
                var lopHocPhans = _context.LopHocPhan
                    .AsNoTracking()
                    .Where(lhp => !lhp.IsDeleted)
                    .Include(lhp => lhp.HocPhan)
                        .ThenInclude(hp => hp.MonHoc)
                    .Include(lhp => lhp.PhanCongGiangDays)
                        .ThenInclude(pc => pc.GiangVien)
                    .Include(lhp => lhp.DangKyHocPhans)
                    .ToList();

                // 4. Ghép ViewModel trong bộ nhớ
                var model = hocKys
                    .Select(hk => new HocKyLopHocPhanViewModel
                    {
                        HocKy = hk,

                        Khoas = khoas
                            .Select(k => new KhoaLopHocPhanViewModel
                            {
                                Khoa = k,

                                LopHocPhans = lopHocPhans
                                    .Where(lhp =>
                                        lhp.HocPhan != null &&
                                        lhp.HocPhan.HocKy != null &&
                                        lhp.HocPhan.MonHoc != null &&
                                        lhp.HocPhan.HocKy.Id == hk.Id &&
                                        lhp.HocPhan.MonHoc.IdKhoa == k.Id)
                                    .Select(lhp => new LopHocPhanViewModel
                                    {
                                        Id = lhp.Id,
                                        TenLopHocPhan = lhp.TenLopHocPhan,
                                        PhanCongGiangDays = lhp.PhanCongGiangDays,

                                        IsDangKy = IdSinhVien.HasValue &&
                                            lhp.DangKyHocPhans.Any(dk =>
                                                dk.IdSinhVien == IdSinhVien.Value)
                                    })
                                    .ToList()
                            })
                            .Where(vm => vm.LopHocPhans.Any())
                            .ToList()
                    })
                    .Where(vm => vm.Khoas.Any())
                    .ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong Index");
                return RedirectToAction("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();  

            if (returnUrl != null)
            {
                return Redirect(returnUrl);  
            }

            return RedirectToAction("Index", "Home", new { area = "" });  
        }
    }
}
