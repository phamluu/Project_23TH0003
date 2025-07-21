using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;
namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class CauHinh_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public CauHinh_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        
        #region Cấu hình

        

        public IActionResult Index()
        {
            
            var model = _context.CauHinhHeThong.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.CauHinhHeThong.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenCauHinh.Contains(searchValue) || x.MaCauHinh.Contains(searchValue));
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
                        "/Admin/CauHinh_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/CauHinh_23TH0003/Edit");
                    return new CauHinhViewModel
                    {
                        Id = x.Id,
                        MaCauHinh = x.MaCauHinh,
                        TenCauHinh = x.TenCauHinh,
                        MoTa = x.MoTa,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CauHinhHeThong model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.CauHinhHeThong.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm cấu hình thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
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
            var model = _context.CauHinhHeThong.Find(id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: HocKy_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CauHinhHeThong model)
        {
            try
            {
                var data = _context.CauHinhHeThong.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Cấu hình không tồn tại";
                    return View(model);
                }
                data.MaCauHinh = model.MaCauHinh;
                data.TenCauHinh = model.TenCauHinh;
                data.MoTa = model.MoTa;
                _context.Update(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật cấu hình thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
            TempData["ErrorMessage"] = "Cập nhật cấu hình không thành công";
            return Json(new { status = false, message = TempData["ErrorMessage"] });
        }

        // GET: HocKy_23TH0003Controller/Delete/5


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var cauHinh = _context.CauHinhHeThong.Find(id);
                if (cauHinh == null)
                {
                    TempData["ErrorMessage"] = "Cấu hình không tồn tại";
                    RedirectToAction("Index");
                }

                _context.CauHinhHeThong.Remove(cauHinh);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa cấu hình thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Lỗi khi xóa: " + ex.Message });
            }

        }

        #endregion

        #region Cài đặt hệ thống

       
        
        public ActionResult CaiDat()
        {
            List<CauHinhViewModel> ch = new List<CauHinhViewModel>();
            ch.Add(new CauHinhViewModel() { MaCauHinh = "smtp_user", TenCauHinh = "Email gửi SMTP", LoaiCauHinh = LoaiCauHinh.Text });
            ch.Add(new CauHinhViewModel() { MaCauHinh = "smtp_pass", TenCauHinh = "Mật khâu Email SMTP", LoaiCauHinh = LoaiCauHinh.Text });
            ch.Add(new CauHinhViewModel() { MaCauHinh = "dropbox_token", TenCauHinh = "Dropbox token", LoaiCauHinh = LoaiCauHinh.Text });
            ch.Add(new CauHinhViewModel() { MaCauHinh = "HocPhi", TenCauHinh = "Học phí 1 tín chỉ", LoaiCauHinh = LoaiCauHinh.Number });
            ch.Add(new CauHinhViewModel() { MaCauHinh = "Banner", TenCauHinh = "Banner", LoaiCauHinh = LoaiCauHinh.Image });
            ch.Add(new CauHinhViewModel() { MaCauHinh = "Footer", TenCauHinh = "Nội dung chân web", LoaiCauHinh = LoaiCauHinh.Editor });
            var model = ch;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaiDat(List<CauHinhHeThong> model)
        {
            _context.CauHinhHeThong.UpdateRange(model);
            _context.SaveChanges();
            return View(model);
        }
        #endregion
    }
}
