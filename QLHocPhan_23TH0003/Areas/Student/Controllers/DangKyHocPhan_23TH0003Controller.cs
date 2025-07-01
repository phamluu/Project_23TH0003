using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class DangKyHocPhan_23TH0003Controller : BaseStudentController
    {
        private readonly MainDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public DangKyHocPhan_23TH0003Controller(MainDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public ActionResult Index()
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == CurrentUserId);
            if (sinhVien == null)
            {
                TempData["ErrorMessage"] = "Sinh viên chưa có hồ sơ";
            }
            var model = _context.DangKyHocPhan
                .Include(x => x.LopHocPhan).ThenInclude(x => x.HocPhan).ThenInclude(x => x.HocKy)
                .Include(x => x.LopHocPhan).ThenInclude(x => x.PhanCongGiangDays).ThenInclude(x => x.GiangVien)
                .Include(x => x.SinhVien).Where(x => x.IdSinhVien == sinhVien.Id).ToList();
            return View(model);
        }

        // GET: DangKyHocPhan_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DangKyHocPhan_23TH0003Controller/Create
        public ActionResult Create(int IdLopHocPhan)
        {
            ViewBag.IdLopHocPhan = IdLopHocPhan;
            return View();
        }

        // POST: DangKyHocPhan_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DangKyHocPhan model)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);
                if (sinhVien == null)
                {
                    TempData["ErrorMessage"] = "Sinh viên chưa có hồ sơ";
                    return View(model);
                }
                model.IdSinhVien = sinhVien.Id;
                _context.DangKyHocPhan.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đăng ký học phần thành công";
            }
            catch
            {
                TempData["ErrorMessage"] = "Đăng ký học phần không thành công";
            }
            return RedirectToAction("Details", "LopHocPhan_23TH0003", new { id = model.IdLopHocPhan });
        }

        // GET: DangKyHocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DangKyHocPhan_23TH0003Controller/Edit/5
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

        // Hủy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            var dk = _context.DangKyHocPhan.Find(id);
            if (dk == null)
            {
                TempData["ErrorMessage"] = "Đăng ký lớp học phần không tìm thấy";
                return RedirectToAction("Index");
            }
            if (dk.TrangThai != (int)TrangThaiDangKy.ChoDuyet)
            {
                TempData["ErrorMessage"] = "không được hủy lớp học phần " + EnumExtensions.GetDisplayName((TrangThaiDangKy)dk.TrangThai);
                return RedirectToAction("Index");
            }
            dk.TrangThai = (int)TrangThaiDangKy.DaHuy;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Hủy đăng ký học phần thành công";
            return RedirectToAction("Index");
        }

    }
}
