using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class LopHoc_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;

        public LopHoc_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: LopHoc_23TH0003Controller
        public ActionResult Index()
        {
            var model = _context.Lop.Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }

        // POST: LopHoc_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lop model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.Lop.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm lớp thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                return Json(new { status = false, message = "Có lỗi xảy ra" });
            }
        }

        // GET: LopHoc_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.Lop.Find(id);
            return View(model);
        }

        // POST: LopHoc_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lop model)
        {
            try
            {
                var data = _context.Lop.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Lớp không tồn tại";
                    return View(model);
                }
                data.TenLop = model.TenLop;
                data.MaLop = model.MaLop;
                data.IdKhoa = model.IdKhoa;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật lớp thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Console.WriteLine("Lỗi" + model.Id);
            }
            TempData["ErrorMessage"] = "Cập nhật lớp không thành công";
            return View(model);
        }

       

        // POST: LopHoc_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var lop = _context.Lop.Find(id);
                if (lop == null)
                    return Json(new { status = false, message = "Lớp không tồn tại." });
                lop.IsDeleted = true;
                _context.SaveChanges();

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Lỗi khi xóa: " + ex.Message });
            }
        }
    }
}
