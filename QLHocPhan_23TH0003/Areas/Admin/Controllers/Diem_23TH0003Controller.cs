using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
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
        public ActionResult Index()
        {
            var model = _context.DangKyHocPhan.Include(x => x.LopHocPhan)
                                              .Include(x => x.SinhVien).ThenInclude(x => x.Lop)
                                              .Include(x =>x.Diem).ToList();
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
