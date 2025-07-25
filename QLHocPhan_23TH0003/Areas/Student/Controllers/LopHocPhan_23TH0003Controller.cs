using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class LopHocPhan_23TH0003Controller : BaseStudentController
    {
        private readonly MainDbContext _context;
        public LopHocPhan_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: LopHocPhan_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);

                if (sinhVien == null)
                {
                    TempData["ErrorMessages"] = new List<string> { "Không tìm thấy hồ sơ sinh viên tương ứng với tài khoản hiện tại." };
                    return View();
                }

                var lhp = _context.LopHocPhan
                    .Where(l => l.Id == id)
                    .Include(l => l.HocPhan).ThenInclude(h => h.HocKy)
                    .Include(l => l.PhanCongGiangDays).ThenInclude(pcgd => pcgd.GiangVien)
                    .FirstOrDefault();

                if (lhp == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy lớp học phần
                }

                var baiHoc = _context.BaiHoc
                    .Where(bh => bh.IdLopHocPhan == id)
                    .Select(x => new BaiHocViewModel
                    {
                        Id = x.Id,
                        IdLopHocPhan = x.IdLopHocPhan,
                        TenBaiHoc = x.TenBaiHoc,
                        NoiDung = x.NoiDung,
                        DropboxFile = DropboxParserHelper.Parse(x.TaiLieu),
                        DropboxVideo = DropboxParserHelper.Parse(x.Video)
                    }).ToList(); 

                // Kiểm tra sinh viên đã đăng ký chưa
                bool isDangKy = _context.DangKyHocPhan.Any(dk =>
                    dk.IdSinhVien == sinhVien.Id &&
                    dk.IdLopHocPhan == lhp.Id &&
                    dk.TrangThai == (int)TrangThaiDangKy.DaDangKy);

                HocViewModel model = new HocViewModel
                {
                    LopHocPhan = lhp,
                    BaiHocs = isDangKy ? baiHoc : null
                };

                ViewBag.IsDangKy = isDangKy;

                return View(model);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { $"Lỗi chính: {ex.Message}" };
                var inner = ex.InnerException;
                while (inner != null)
                {
                    errors.Add($"Lỗi lồng: {inner.Message}");
                    inner = inner.InnerException;
                }

                TempData["ErrorMessages"] = errors;
                return View();
            }
        }


        // GET: LopHocPhan_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Create
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

        // GET: LopHocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Edit/5
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

        // GET: LopHocPhan_23TH0003Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Delete/5
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
    }
}
