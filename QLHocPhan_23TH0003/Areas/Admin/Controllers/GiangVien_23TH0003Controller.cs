using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class GiangVien_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public GiangVien_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }

        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            var model = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }

        // GET: GiangVien_23TH0003/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GiangVien_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
            return View();
        }

        // POST: GiangVien_23TH0003/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GiangVien model)
        {
            try
            {
                _context.GiangVien.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm giảng viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Thêm giảng viên không thành công";
                return View(model);
            }
        }

        // GET: GiangVien_23TH0003/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.GiangVien.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
            return View(model);
        }

        // POST: GiangVien_23TH0003/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GiangVien model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật giảng viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }



        // POST: GiangVien_23TH0003/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Giảng viên không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa giảng viên thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục giảng viên thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon != null)
            {
                _context.GiangVien.Remove(mon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn giảng viên thành công";
            }
            return RedirectToAction("Trash");
        }
    }
}
