using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class ChinhSachHocPhiController : BaseAdminController
    {
        private readonly MainDbContext _context;
        public ChinhSachHocPhiController(MainDbContext contetxt)
        {
            _context = contetxt;
        }
        public IActionResult Index(int? IdHocKy)
        {
            // Load toàn bộ danh sách học kỳ => thêm/ cập nhật học phí cho từng học kỳ
            var hocKys = _context.HocKy.ToList();
            ViewBag.IdHocKy = IdHocKy;
            ViewBag.HocKys = hocKys;
            var chinhSach = _context.ChinhSachHocPhi.FirstOrDefault(x => x.IdHocKy == IdHocKy);
            return View(chinhSach);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eđit(ChinhSachHocPhi model)
        {
            try
            {
                var chinhSach = _context.ChinhSachHocPhi.FirstOrDefault(x => x.IdHocKy == model.IdHocKy);
                if (chinhSach != null)
                {
                    chinhSach.DonGiaTinChi = model.DonGiaTinChi;
                    _context.Update(chinhSach);
                    TempData["SuccessMessage"] = "Cập nhật thành công";
                }
                else
                {
                    _context.ChinhSachHocPhi.Add(model);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi cập nhật: " + ex.Message;
            }
            return RedirectToAction("Index", new { IdHocKy = model.IdHocKy});
        }
    }
}
