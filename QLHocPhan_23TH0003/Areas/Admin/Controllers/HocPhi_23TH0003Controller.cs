using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class HocPhi_23TH0003Controller : BaseAdminController
    {
        private readonly QuanLyHocPhanDbContext _context;
        public HocPhi_23TH0003Controller(QuanLyHocPhanDbContext contetxt)
        {
            _context = contetxt;
        }
        public ActionResult Index()
        {
            var model = _context.ThanhToanHocPhi.Include(x => x.SinhVien).Include(x => x.HocKy).ToList();
            return View(model);
        }

        // GET: HocPhi_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            var model = _context.ThanhToanHocPhi.Include(x => x.SinhVien).ThenInclude(x => x.Lop).ThenInclude(x => x.Khoa)
                                                .Include(x => x.HocKy).FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTrangThai(int id, int Status)
        {
            var model = _context.ThanhToanHocPhi.Find(id);
            if(model == null)
            {
                return NotFound();
            }
            model.TrangThai = Status;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Cập nhật thành công";
            return RedirectToAction("Details", new { id = id });
        }
        // GET: HocPhi_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HocPhi_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: HocPhi_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _context.ThanhToanHocPhi.Find(id);
                if (model != null)
                {
                   _context.Remove(model);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa hóa đơn thành công";
                }
                
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Xóa hóa đơn thất bại";
            }
            
            return RedirectToAction("Index");
        }
    }
}
