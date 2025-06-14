using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class HocKy_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly IRazorViewToStringRenderer _renderer;
        private readonly LinkGenerator _linkGenerator;
        public HocKy_23TH0003Controller(MainDbContext context, IRazorViewToStringRenderer renderer, LinkGenerator linkGenerator)
        {
            _context = context;
            _renderer = renderer;
            _linkGenerator = linkGenerator;
        }
        // GET: HocKy_23TH0003Controller
        public ActionResult Index()
        {
            var model = _context.HocKy.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.HocKy.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenHocKy.Contains(searchValue) || x.MaHocKy.Contains(searchValue));
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
                        "/Admin/HocKy_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/HocKy_23TH0003/Edit");
                    return new HocKyViewModel
                    {
                        Id = x.Id,
                        MaHocKy = x.MaHocKy,
                        TenHocKy = x.TenHocKy,
                        NamHoc = x.NamHoc,
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


        // POST: HocKy_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HocKy model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.HocKy.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm học kỳ thành công";
                return Json(new {status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra";
                return Json(new { status = false, message = TempData["ErrorMessage"] });
            }
        }

        // GET: HocKy_23TH0003Controller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = _context.HocKy.Find(id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: HocKy_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,MaHocKy,TenHocKy,NamHoc,ThuTu")] HocKy model)
        {
            try
            {
                var data = _context.HocKy.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Học kỳ không tồn tại";
                    return View(model);
                }
                data.MaHocKy = model.MaHocKy;
                data.TenHocKy = model.TenHocKy;
                data.NamHoc = model.NamHoc;
                data.ThuTu = model.ThuTu;
                data.NgayCapNhat = DateTime.UtcNow;

                _context.Update(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật học kỳ thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
            TempData["ErrorMessage"] = "Cập nhật học kỳ không thành công";
            return Json(new { status = false, message = TempData["ErrorMessage"] });
        }

        // GET: HocKy_23TH0003Controller/Delete/5
        

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var hocKy = _context.HocKy.Find(id);
                if (hocKy == null)
                {
                    TempData["ErrorMessage"] = "Học kỳ không tồn tại";
                    RedirectToAction("Index");
                }
                    
                _context.HocKy.Remove(hocKy);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa học kỳ thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Lỗi khi xóa: " + ex.Message });
            }
            
        }

    }
}
