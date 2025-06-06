using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HocKy_23TH0003Controller : Controller
    {
        private readonly MainDbContext _context;
        public HocKy_23TH0003Controller(MainDbContext context)
        {
            _context = context;
        }
        // GET: HocKy_23TH0003Controller
        public ActionResult Index()
        {
            var model = _context.HocKy.ToList();
            return View(model);
        }

        // GET: HocKy_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HocKy_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HocKy_23TH0003Controller/Create
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

        // GET: HocKy_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HocKy_23TH0003Controller/Edit/5
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

        // GET: HocKy_23TH0003Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HocKy_23TH0003Controller/Delete/5
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
