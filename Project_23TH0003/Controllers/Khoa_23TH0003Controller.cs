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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName,DeanID")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Created_at = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thêm khoa thành công!";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Không thể thêm khoa";
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName", department.DeanID);
            return View(department);
        }

        [Authorize(Roles = "admin")]
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
            var instructors = db.Instructors.Where(x => x.DepartmentID == id);
            ViewBag.DeanID = new SelectList(instructors, "InstructorID", "FullName", department.DeanID);
            return View(department);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName,DeanID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật khoa thành công!";
                return RedirectToAction("Index");
            }
            ViewBag.DeanID = new SelectList(db.Instructors, "InstructorID", "FullName", department.DeanID);
            TempData["ErrorMessage"] = "Cập nhật khoa thất bại!";
            return View(department);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            try
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Xóa khoa thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            TempData["ErrorMessage"] = "Xóa khoa thất bại!";
            return View(department);
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
