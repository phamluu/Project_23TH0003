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
    public class Khoa_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        [HttpGet]
        public ActionResult TimKiemNC(string DepartmentName = "")
        {
            ViewBag.DepartmentName = DepartmentName;
            var khoas = db.Departments.SqlQuery("EXEC Khoa_TimKiem @DepartmentName",
                new SqlParameter("@DepartmentName", (object)DepartmentName ?? DBNull.Value)).ToList();

            if (khoas.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", khoas);
        }
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.Instructor).OrderByDescending(d => d.Created_at);
            return View(departments.ToList());
        }

        // GET: Khoa_23TH0003/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Khoa_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName");
            return View();
        }

        // POST: Khoa_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName,DeanID")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Created_at = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName", department.DeanID);
            return View(department);
        }

        // GET: Khoa_23TH0003/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName", department.DeanID);
            return View(department);
        }

        // POST: Khoa_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName,DeanID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName", department.DeanID);
            return View(department);
        }

        // GET: Khoa_23TH0003/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Khoa_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
