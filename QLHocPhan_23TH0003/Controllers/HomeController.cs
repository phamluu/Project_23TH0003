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
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, MainDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                if (!_userManager.Users.Any())
                    return Redirect("/Identity/Account/Register");

                var model = _context.HocKy.AsNoTracking()
                             .Where(hk => hk.HocPhans.Any(hp => !hp.IsDeleted && hp.LopHocPhans.Any(lhp => !lhp.IsDeleted)))
                             .OrderByDescending(hk => hk.ThuTu)
                             .Select(hk => new HocKyLopHocPhanViewModel
                             {
                                 HocKy = hk,
                                 Khoas = _context.Khoa
                                     .Select(k => new KhoaLopHocPhanViewModel
                                     {
                                         Khoa = k,
                                         LopHocPhans = _context.LopHocPhan
                                                         .Where(lhp =>
                                                             !lhp.IsDeleted &&
                                                             lhp.HocPhan.HocKy.Id == hk.Id &&
                                                             lhp.HocPhan.MonHoc.IdKhoa == k.Id)
                                                         .Include(lhp => lhp.PhanCongGiangDays)
                                                             .ThenInclude(pc => pc.GiangVien)
                                                         .Include(lhp => lhp.HocPhan)
                                                             .ThenInclude(hp => hp.MonHoc)
                                                         .ToList()
                                     }).Where(vm => vm.LopHocPhans.Any()) // chỉ lấy khoa có lớp học phần
                                     .ToList()
                             })
                             .Where(vm => vm.Khoas.Any()) // chỉ lấy học kỳ có dữ liệu
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
    }
}
