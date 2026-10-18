using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using System.Net.WebSockets;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class Diem_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public  Diem_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: XemDiem_23TH0003Controller
        public ActionResult Index(string? MSSV,int? idLopHoc,int? idLopHocPhan)
        {
            var lopHocs = _context.Lop.Where(x => x.IsDeleted != true).ToList();
            ViewBag.IdLopHoc = new SelectList(lopHocs, "Id", "TenLop", idLopHoc);
            var lopHocphans = _context.LopHocPhan.Where(x => x.IsDeleted != true).ToList();
            ViewBag.IdLopHocPhan = new SelectList(lopHocphans, "Id", "TenLopHocPhan", idLopHocPhan);
            var query = _context.DangKyHocPhan
                .Include(x => x.LopHocPhan)
                .Include(x => x.SinhVien)
                    .ThenInclude(x => x.Lop)
                .Include(x => x.Diem)
                .AsQueryable();

            if (!string.IsNullOrEmpty(MSSV))
            {
                query = query.Where(x => x.SinhVien.MaSinhVien.Contains(MSSV));
            }

            if (idLopHoc.HasValue)
            {
                query = query.Where(
                    x => x.SinhVien.Lop.Id == idLopHoc.Value);
            }

            if (idLopHocPhan.HasValue)
            {
                query = query.Where(
                    x => x.LopHocPhan.Id == idLopHocPhan.Value);
            }

            var model = query.ToList();

            return View(model);
        }
        // GET: XemDiem_23TH0003Controller/Details/5


        // GET: XemDiem_23TH0003Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: XemDiem_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
