using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class LopHocPhan_23TH0003Controller : BaseAdminController
    {
        // GET: LopHocPhan_23TH0003Controller
        public ActionResult Index()
        {
            return View();
        }

        // GET: LopHocPhan_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LopHocPhan_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Create
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

        // GET: LopHocPhan_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Edit/5
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

        // GET: LopHocPhan_23TH0003Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LopHocPhan_23TH0003Controller/Delete/5
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
