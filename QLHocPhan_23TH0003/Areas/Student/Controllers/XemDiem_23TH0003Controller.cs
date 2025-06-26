using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class XemDiem_23TH0003Controller : BaseStudentController
    {
        private readonly MainDbContext _context;

        public XemDiem_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: XemDiemController
        public ActionResult Index()
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == CurrentUserId);
            var model = _context.DangKyHocPhan.Where(x => x.IdSinhVien == sinhVien.Id)
                .Include(x => x.LopHocPhan).ThenInclude(x => x.HocPhan).ThenInclude(x => x.HocKy)
                .Include(x => x.Diem).Where(x => x.TrangThai == (int)TrangThaiDangKy.DaDangKy).ToList();
            return View(model);
        }

        // GET: XemDiemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: XemDiemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XemDiemController/Create
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

        // GET: XemDiemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: XemDiemController/Edit/5
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

        // GET: XemDiemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: XemDiemController/Delete/5
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
