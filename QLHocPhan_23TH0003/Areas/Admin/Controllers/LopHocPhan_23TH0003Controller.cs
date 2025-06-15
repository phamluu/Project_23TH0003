using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class LopHocPhan_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public LopHocPhan_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var model = _context.LopHocPhan.Include(x => x.HocPhan).Where(x => x.IsDeleted != true).ToList();
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
            var hocPhans = _context.HocPhan.ToList();
            ViewBag.IdHocPhan = new SelectList(hocPhans, "Id", "TenHocPhan");
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LopHocPhan model)
        {
            try
            {
                _context.LopHocPhan.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm lớp học phần thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Thêm lớp học phần không thành công";
                return View(model);
            }
        }

        // GET: LopHocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.LopHocPhan.Find(id);
            var hocPhans = _context.HocPhan.ToList();
            ViewBag.IdHocPhan = new SelectList(hocPhans, "Id", "TenHocPhan");
            return View(model);
        }

        // POST: LopHocPhan_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LopHocPhan model)
        {
            try
            {
                _context.LopHocPhan.Update(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật lớp học phần thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Cập nhật lớp học phần không thành công";
                return View(model);
            }
        }


        // POST: LopHocPhan_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.LopHocPhan.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Lớp học phần không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa lớp học phần thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.LopHocPhan.Include(x => x.HocPhan).Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var mon = _context.LopHocPhan.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục lớp học phần thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.LopHocPhan.Find(id);
            if (mon != null)
            {
                _context.LopHocPhan.Remove(mon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn lớp học phần thành công";
            }
            return RedirectToAction("Trash");
        }
    }
}
