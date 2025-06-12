using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;

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

        // GET: HocKy_23TH0003Controller/Create
        public async Task<ActionResult> Create()
        {
            var html = await _renderer.RenderViewToStringAsync("Create", null, area: "Admin", controller: "HocKy_23TH0003");

            return Json(new { html });
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
                return Json(new { status = false, message = "" });
            }
        }

        // GET: HocKy_23TH0003Controller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = _context.HocKy.Find(id);
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
                    return View(model);
                data.MaHocKy = model.MaHocKy;
                data.TenHocKy = model.TenHocKy;
                data.NamHoc = model.NamHoc;
                data.ThuTu = model.ThuTu;
                data.NgayCapNhat = DateTime.UtcNow;

                _context.Update(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật học kỳ thành công";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
            TempData["ErrorMessage"] = "Cập nhật học kỳ không thành công";
            return View(model);
        }

        // GET: HocKy_23TH0003Controller/Delete/5
        

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var hocKy = _context.HocKy.Find(id);
                if (hocKy == null)
                    return Json(new { status = false, message = "Học kỳ không tồn tại." });

                _context.HocKy.Remove(hocKy);
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
