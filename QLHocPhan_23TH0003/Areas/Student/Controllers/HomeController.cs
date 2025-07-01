using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class HomeController : BaseStudentController
    {
        private readonly MainDbContext _context;
        public HomeController(MainDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == CurrentUserId);
            if (sinhVien == null)
            {
                TempData["ErrorMessage"] = "Sinh viên chưa có hồ sơ";
                return View();
            }
            
            var model = _context.LopHocPhan.Include(x => x.HocPhan).ThenInclude(x => x.HocKy)
                .Include(x => x.HocPhan).ThenInclude(x => x.MonHoc).ThenInclude(x => x.Khoa)
                .Include(x => x.PhanCongGiangDays).ThenInclude(x => x.GiangVien)
                .Where(x => x.DangKyHocPhans.Any(dk => dk.TrangThai == (int)TrangThaiDangKy.DaDangKy && dk.IdSinhVien == sinhVien.Id))
                .ToList();
            return View(model);
        }
    }
}
