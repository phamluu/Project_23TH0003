using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Instructor.Controllers
{
    public class PhanCongGiangDay_23TH0003Controller : BaseInstructorController
    {
        private readonly QLHocPhan_23TH0003.Data.MainDbContext _context;
        public PhanCongGiangDay_23TH0003Controller(QLHocPhan_23TH0003.Data.MainDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var GiangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == UserId);
            var model = _context.PhanCongGiangDay.Include(p => p.LopHocPhan).ThenInclude(p => p.HocPhan)
                                                                            .ThenInclude(p => p.HocKy)
                                                  .Include(p => p.LopHocPhan).ThenInclude(p => p.DangKyHocPhans)
                .Where(p => p.IdGiangVien == GiangVien.Id).ToList();
            return View(model);
        }

        // GET: LopHocPhan_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
