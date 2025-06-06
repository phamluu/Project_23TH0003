using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class GiangVien_23TH0003 : Controller
    {
        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            return View();
        }

        // GET: GiangVien_23TH0003/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GiangVien_23TH0003/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiangVien_23TH0003/Create
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

        // GET: GiangVien_23TH0003/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GiangVien_23TH0003/Edit/5
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

        // GET: GiangVien_23TH0003/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GiangVien_23TH0003/Delete/5
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
