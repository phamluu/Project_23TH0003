using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    [Authorize(Roles = "admin")]
    public class KhoaHoc_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        [HttpGet]

        public ActionResult TimKiemNC(string CourseName = "", int? DepartmentID = null, int? Credits = null)
        {
            ViewBag.CourseName = CourseName;
            ViewBag.Credits = Credits;
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", DepartmentID);

            var khoahocs = db.Courses.SqlQuery("EXEC KhoaHoc_TimKiem @CourseName, @DepartmentID, @Credits",
                new SqlParameter("@CourseName", (object)CourseName ?? DBNull.Value),
                new SqlParameter("@DepartmentID", (object)DepartmentID ?? DBNull.Value),
                new SqlParameter("@Credits", (object)Credits ?? DBNull.Value)).ToList();

            if (khoahocs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", khoahocs);
        }
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View(courses.ToList());
        }

        // GET: KhoaHoc_23TH0003/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: KhoaHoc_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: KhoaHoc_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CourseName,Credits,DepartmentID")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", cours.DepartmentID);
            return View(cours);
        }

        // GET: KhoaHoc_23TH0003/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", cours.DepartmentID);
            return View(cours);
        }

        // POST: KhoaHoc_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,Credits,DepartmentID")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", cours.DepartmentID);
            return View(cours);
        }

        // GET: KhoaHoc_23TH0003/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: KhoaHoc_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
