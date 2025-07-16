using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;

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
            var model = _context.Khoa.Where(x => x.IsDeleted != true).OrderByDescending(x => x.NgayTao).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.Khoa.Include(x => x.TruongKhoa).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenKhoa.Contains(searchValue) || x.MaKhoa.Contains(searchValue));
            }

            var recordsTotal = query.Count();

            // 👉 Thêm sắp xếp giảm dần theo NgayTao
            query = query.OrderByDescending(x => x.NgayTao);

            var antiforgery = HttpContext.RequestServices.GetService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>();
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            string token = tokens.RequestToken;

            var data = query.Skip(start).Take(length).ToList()
                .Select(x =>
                {
                    var deleteForm = ButtonHelper.BuildDeleteFormHtml(
                        x.Id,
                        "/Admin/Khoa_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/Khoa_23TH0003/Edit");
                    return new KhoaViewModel
                    {
                        Id = x.Id,
                        MaKhoa = x.MaKhoa,
                        TenKhoa = x.TenKhoa,
                        TenTruongKhoa = x.TruongKhoa?.HoTen ?? "",
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
            var truongKhoaList = _context.GiangVien.Where(nv => nv.IdKhoa == id)
                                .Select(nv => new { nv.Id, nv.HoTen })
                                .ToList();

            ViewBag.IdTruongKhoa = new SelectList(truongKhoaList, "Id", "HoTen", model.IdTruongKhoa);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Khoa model)
        {
            try
            {
                var data = _context.Khoa.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Khoa không tồn tại";
                    return View(model);
                }
                data.MaKhoa = model.MaKhoa;
                data.TenKhoa = model.TenKhoa;
                data.IdTruongKhoa = model.IdTruongKhoa;
                data.NgayCapNhat = DateTime.UtcNow;

                _context.Update(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật khoa thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
            TempData["ErrorMessage"] = "Cập nhật khoa không thành công";
            return Json(new { status = false, message = TempData["ErrorMessage"] });
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var khoa = _context.Khoa.Find(id);
                if (khoa == null)
                {
                    TempData["ErrorMessage"] = "Khoa không tồn tại";
                    return RedirectToAction("Index");
                }

                // ✅ Kiểm tra ràng buộc trước khi xóa
                var errors = new List<string>();
                if (_context.Lop.Any(x => x.IdKhoa == id))
                    errors.Add("Tồn tại lớp thuộc khoa");
                if (_context.MonHoc.Any(x => x.IdKhoa == id))
                    errors.Add("Tồn tại môn học thuộc khoa");
                if (_context.GiangVien.Any(x => x.IdKhoa == id))
                    errors.Add("Tồn tại giảng viên thuộc khoa");

                if (errors.Any())
                {
                    TempData["ErrorMessage"] = "Không thể xóa khoa. " + string.Join("; ", errors);
                    return RedirectToAction("Index");
                }

                // ✅ Xóa nếu không có ràng buộc
                _context.Khoa.Remove(khoa);
                var affectedRows = _context.SaveChanges();

                if (affectedRows > 0)
                {
                    TempData["SuccessMessage"] = "Xóa khoa thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không có thay đổi nào được lưu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
