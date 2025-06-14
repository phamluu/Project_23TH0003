using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class HocPhan_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public HocPhan_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: HocPhan_23TH0003Controller
        public ActionResult Index()
        {
            ViewBag.IdMonHoc = _context.MonHoc.Where(x => x.IsDeleted != true).ToList();
            ViewBag.IdHocKy = _context.HocKy.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.HocPhan.Include(x => x.MonHoc).Include(x => x.HocKy).Where(x => x.IsDeleted != true).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenHocPhan.Contains(searchValue) || x.MaHocPhan.Contains(searchValue));
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
                        "/Admin/HocPhan_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/HocPhan_23TH0003/Edit");
                    return new HocPhanViewModel
                    {
                        Id = x.Id,
                        MaHocPhan = x.MaHocPhan,
                        TenHocPhan = x.TenHocPhan,
                        TenMonHoc = x.MonHoc?.TenMonHoc ?? "",
                        TenHocKy = x.HocKy?.TenHocKy ?? "",
                        SoTinChi = x.SoTinChi,
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

        // POST: HocPhan_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HocPhan model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.HocPhan.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm học phần thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                return Json(new { status = false, message = "Có lỗi xảy ra" });
            }
        }

        // GET: HocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.HocPhan.Find(id);
            if (model == null)
                return Json(new { status = false, message = "Học phần không tồn tại." });
            var viewmodel = new HocPhanViewModel();
            viewmodel.Id = model.Id;
            viewmodel.TenHocPhan = model.TenHocPhan;
            viewmodel.MaHocPhan = model.MaHocPhan;
            viewmodel.IdMonHoc = model.IdMonHoc;
            viewmodel.IdHocKy = model.IdHocKy;
            viewmodel.SoTinChi = model.SoTinChi;
            viewmodel.HocKies = _context.HocKy.ToList();
            viewmodel.MonHocs = _context.MonHoc.Where(x => x.IsDeleted != true).ToList();
            return PartialView("Edit", viewmodel);
        }

        // POST: HocPhan_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HocPhan model)
        {
            try
            {
                var data = _context.HocPhan.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Học phần không tồn tại";
                    return View(model);
                }
                data.MaHocPhan = model.MaHocPhan;
                data.TenHocPhan = model.TenHocPhan;
                data.IdHocKy = model.IdHocKy;
                data.IdMonHoc = model.IdMonHoc;
                data.SoTinChi = model.SoTinChi;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật học phần thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                Console.WriteLine("Lỗi" + model.Id);
            }
            TempData["ErrorMessage"] = "Cập nhật học phần không thành công";
            return Json(new { status = false, message = "Có lỗi xảy ra" });
        }


        // POST: HocPhan_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.HocPhan.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Học phần không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa học phần thành công";
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
            var mon = _context.HocPhan.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục học phần thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.HocPhan.Find(id);
            if (mon != null)
            {
                _context.HocPhan.Remove(mon);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa vĩnh viễn học phần thành công";
            }
            return RedirectToAction("Trash");
        }
    }
}
