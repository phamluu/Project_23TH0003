using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class Khoa_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;

        public Khoa_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var model = _context.Khoa.Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }


        // POST: Khoa_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Khoa model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.Khoa.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm khoa thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                return Json(new { status = false, message = "Có lỗi xảy ra" });
            }
        }

        // GET: Khoa_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.Khoa.Find(id);
            return View(model);
        }

        // POST: Khoa_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Khoa model)
        {
            try
            {
                var data = _context.Khoa.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Khoa không tồn tại";
                    return View(model);
                }
                data.TenKhoa = model.TenKhoa;
                data.MaKhoa = model.MaKhoa;
                data.IdTruongKhoa = model.IdTruongKhoa;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật khoa thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Console.WriteLine("Lỗi" + model.Id);
            }
            TempData["ErrorMessage"] = "Cập nhật khoa không thành công";
            return View(model);
        }

        // POST: Khoa_23TH0003Controller/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var khoa = _context.Khoa.Find(id);
                if (khoa == null)
                    return Json(new { status = false, message = "Khoa không tồn tại." });
                khoa.IsDeleted = true;
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
