using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class DangKyHocPhan_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;

        public DangKyHocPhan_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: DangKyHocPhan_23TH0003Controller
        public ActionResult Index()
        {
            var model = _context.DangKyHocPhan.Include(x => x.LopHocPhan).ThenInclude(x => x.HocPhan)
                .ThenInclude(x => x.HocKy)
                .Include(x => x.SinhVien).ThenInclude(x => x.Lop).ToList();
            return View(model);
        }

        // GET: DangKyHocPhan_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DangKyHocPhan_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DangKyHocPhan_23TH0003Controller/Create
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

        // GET: DangKyHocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DangKyHocPhan_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int id, int status)
        {
            var model = _context.DangKyHocPhan.Find(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = "Đăng ký học phần không tồn tại";
                return RedirectToAction("Index");
            }
            string StatusName = EnumExtensions.GetDisplayName((TrangThaiDangKy)status);
            model.TrangThai = status;
            _context.SaveChanges();
            TempData["SuccessMessage"] = StatusName + " đăng ký học phần thành công";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = _context.DangKyHocPhan.Find(id);
            if (model == null)
            {
                TempData["ErrorMessage"] = "Đăng ký học phần không tồn tại";
                return RedirectToAction("Index");
            }
            _context.DangKyHocPhan.Remove(model);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa đăng ký học phần thành công";
            return RedirectToAction("Index");
        }
    }
}
