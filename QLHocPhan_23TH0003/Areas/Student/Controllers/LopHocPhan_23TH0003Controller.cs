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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);

            var lhp = _context.LopHocPhan
            .Where(lhp => lhp.Id == id)
            .Include(lhp => lhp.HocPhan).ThenInclude(hp => hp.HocKy)
            .Include(lhp => lhp.PhanCongGiangDays).ThenInclude(pcgd => pcgd.GiangVien)
            .FirstOrDefault();
            
            if (lhp == null)
            {
                return NotFound(); 
            }
            var baiHoc = _context.BaiHoc.Where(bh => bh.IdLopHocPhan == id).Select(x => new BaiHocViewModel
            {
                Id = x.Id,
                IdLopHocPhan = x.IdLopHocPhan,
                TenBaiHoc = x.TenBaiHoc,
                NoiDung = x.NoiDung,
                DropboxFile = DropboxParserHelper.Parse(x.TaiLieu),
                DropboxVideo = DropboxParserHelper.Parse(x.Video)
            });
            // Kiểm tra sinh viên đã đăng ký chưa
            bool isDangKy = _context.DangKyHocPhan.Any(dk => dk.IdSinhVien == sinhVien.Id 
            && dk.IdLopHocPhan == lhp.Id && dk.TrangThai == (int)TrangThaiDangKy.DaDangKy);

            HocViewModel model = new HocViewModel();
            model.LopHocPhan = lhp;
            if (isDangKy == true)
            {
                model.BaiHocs = baiHoc;
            }
            // Gửi biến vào View nếu cần dùng
            ViewBag.IsDangKy = isDangKy;

            return View(model);
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
