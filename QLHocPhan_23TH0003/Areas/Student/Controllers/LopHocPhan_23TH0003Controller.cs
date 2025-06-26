using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
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
            // Lấy userId từ Claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Lấy sinh viên tương ứng (nếu có)
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);
            
            var model = _context.LopHocPhan
                .Include(lhp => lhp.HocPhan)
                .Include(lhp => lhp.DangKyHocPhans)
                .FirstOrDefault(lhp => lhp.Id == id);

            if (model == null)
            {
                return NotFound(); 
            }

            // Kiểm tra sinh viên đã đăng ký chưa
            bool isDangKy = model.DangKyHocPhans.Any(dk => dk.IdSinhVien == sinhVien.Id);

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
