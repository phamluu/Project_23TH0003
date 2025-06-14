using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;

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
            ViewBag.IdKhoa = _context.Khoa.Where(x => x.IsDeleted != true).ToList();
            var model = _context.Lop.Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.Lop.Include(x => x.Khoa).Where(x => x.IsDeleted != true).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenLop.Contains(searchValue) || x.MaLop.Contains(searchValue));
            }
            var recordsTotal = query.Count();
            var antiforgery = HttpContext.RequestServices.GetService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>();
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            string token = tokens.RequestToken;

            var data = query.Skip(start).Take(length).ToList()
                .Select(x =>
                {
                    var deleteForm = ButtonHelper.BuildDeleteFormHtml(
                        x.Id,
                        "/Admin/LopHoc_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/LopHoc_23TH0003/Edit");
                    return new LopViewModel
                    {
                        Id = x.Id,
                        MaLop = x.MaLop,
                        TenLop = x.TenLop,
                        TenKhoa = x.Khoa?.TenKhoa ?? "",
                        NgayTao = x.NgayTao,
                        Action = editForm + deleteForm
                    };
                }).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
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
            if (model == null)
                return Json(new { status = false, message = "Lớp không tồn tại." });
                var viewmodel = new LopViewModel();
                viewmodel.Id = model.Id;
                viewmodel.TenLop = model.TenLop;
                viewmodel.MaLop = model.MaLop;
                viewmodel.IdKhoa = model.IdKhoa;
                viewmodel.KhoaList = _context.Khoa.Where(x => x.IsDeleted != true).ToList();
            return PartialView("Edit", viewmodel);
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
                TempData["SuccessMessage"] = "Cập nhật lớp thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                Console.WriteLine("Lỗi" + model.Id);
            }
            TempData["ErrorMessage"] = "Cập nhật lớp không thành công";
            return Json(new { status = false, message = "Có lỗi xảy ra" });
        }



        // POST: LopHoc_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var lop = _context.Lop.Find(id);
            if (lop == null)
            {
                TempData["ErrorMessage"] = "Lớp không tồn tại";
                return RedirectToAction("Index");
            }
            lop.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa lớp thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.Lop.Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var lop = _context.Lop.Find(id);
            if (lop != null)
            {
                lop.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục lớp thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        public ActionResult HardDelete(int id)
        {
            var lop = _context.Lop.Find(id);
            if (lop != null)
            {
                _context.Lop.Remove(lop);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn lớp thành công";
            }
            return RedirectToAction("Trash");
        }

    }
}
