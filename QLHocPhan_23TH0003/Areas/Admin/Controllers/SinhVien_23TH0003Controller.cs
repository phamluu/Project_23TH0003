using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class SinhVien_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public SinhVien_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }

        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            var model = _context.SinhVien.Include(x => x.Lop).Where(x => x.IsDeleted != true).ToList();
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
            ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
            return View();
        }

        // POST: GiangVien_23TH0003/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SinhVien model)
        {
            try
            {
                _context.SinhVien.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm sinh viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Thêm sinh viên không thành công";
                return View(model);
            }
        }

        // GET: GiangVien_23TH0003/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.SinhVien.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
            return View(model);
        }

        // POST: GiangVien_23TH0003/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SinhVien model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật sinh viên thành công";
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
            var mon = _context.SinhVien.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Sinh viên không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa sinh viên thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.SinhVien.Include(x => x.Lop).Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var mon = _context.SinhVien.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục sinh viên thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.SinhVien.Find(id);
            if (mon != null)
            {
                _context.SinhVien.Remove(mon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn sinh viên thành công";
            }
            return RedirectToAction("Trash");
        }
    }
}
