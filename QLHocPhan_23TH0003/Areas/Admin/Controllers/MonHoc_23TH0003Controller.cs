using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class MonHoc_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public MonHoc_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: MonHoc_23TH0003Controller
        public ActionResult Index()
        {
            var model = _context.MonHoc.Include(x => x.Khoa).Where(x => x.IsDeleted != true).ToList();
            ViewBag.IdKhoa = _context.Khoa.Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.MonHoc.Include(x => x.Khoa).Where(x => x.IsDeleted != true).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenMonHoc.Contains(searchValue) || x.MaMonHoc.Contains(searchValue));
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
                        "/Admin/MonHoc_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/MonHoc_23TH0003/Edit");
                    return new MonHocViewModel
                    {
                        Id = x.Id,
                        MaMonHoc = x.MaMonHoc,
                        TenMonHoc = x.TenMonHoc,
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
        
        
        // POST: MonHoc_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonHoc model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.MonHoc.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm lớp thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                return Json(new { status = false, message = "Có lỗi xảy ra" });
            }
        }

        // GET: MonHoc_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.MonHoc.Find(id);
            if (model == null)
                return Json(new { status = false, message = "Môn học không tồn tại." });
            var viewmodel = new MonHocViewModel();
            viewmodel.Id = model.Id;
            viewmodel.TenMonHoc = model.TenMonHoc;
            viewmodel.MaMonHoc = model.MaMonHoc;
            viewmodel.IdKhoa = model.IdKhoa;
            viewmodel.Khoas = _context.Khoa.Where(x => x.IsDeleted != true).ToList();
            return PartialView("Edit", viewmodel);
        }

        // POST: MonHoc_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MonHoc model)
        {
            try
            {
                var data = _context.MonHoc.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Môn học không tồn tại";
                    return View(model);
                }
                data.TenMonHoc = model.TenMonHoc;
                data.MaMonHoc = model.MaMonHoc;
                data.IdKhoa = model.IdKhoa;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật môn học thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                Console.WriteLine("Lỗi" + model.Id);
            }
            TempData["ErrorMessage"] = "Cập nhật môn học không thành công";
            return Json(new { status = false, message = "Có lỗi xảy ra" });
        }

        

        // POST: MonHoc_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.MonHoc.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Môn học không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa môn học thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.MonHoc.Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var mon = _context.MonHoc.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục môn học thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.MonHoc.Find(id);
            if (mon != null)
            {
                _context.MonHoc.Remove(mon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn môn học thành công";
            }
            return RedirectToAction("Trash");
        }
    }
}
