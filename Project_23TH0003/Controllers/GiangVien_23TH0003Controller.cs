using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    public class GiangVien_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();
        [Authorize(Roles = "giangvien")]
        public ActionResult Profile()
        {
            var UserID = User.Identity.GetUserId();
            Instructor instructor = db.Instructors.Include(i => i.User).Include(i => i.Department).FirstOrDefault(i => i.UserID.ToString() == UserID);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            var department = db.Departments.Where(d => d.DepartmentID == instructor.DepartmentID).ToList();
            ViewBag.DepartmentID = new SelectList(department, "DepartmentID", "DepartmentName", instructor.DepartmentID);
            var user = db.Users.Where(d => d.UserID == instructor.UserID).ToList();
            ViewBag.UserID = new SelectList(user, "UserID", "Username", instructor.UserID);
            return View(instructor);
        }

        [Authorize(Roles = "giangvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "InstructorID,UserID,DepartmentID,FullName,Phone")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Instructors.Attach(instructor);

                    db.Entry(instructor).Property(i => i.UserID).IsModified = false;
                    db.Entry(instructor).Property(i => i.DepartmentID).IsModified = false;
                    db.Entry(instructor).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Cập nhật giảng viên thành công!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }
            var department = db.Departments.Where(d => d.DepartmentID == instructor.DepartmentID).ToList();
            ViewBag.DepartmentID = new SelectList(department, "DepartmentID", "DepartmentName", instructor.DepartmentID);
            var user = db.Users.Where(d => d.UserID == instructor.UserID).ToList();
            ViewBag.UserID =  new SelectList(user, "UserID", "Username", instructor.UserID);
            return View(instructor);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TimKiemNC(string FullName = "", string Phone = null, int? DepartmentID = null)
        {
            ViewBag.FullName = FullName;
            ViewBag.Phone = Phone;
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", DepartmentID);

            var giangviens = db.Instructors.SqlQuery("EXEC Instructor_TimKiem @FullName, @Phone, @DepartmentID",
                new SqlParameter("@FullName", (object)FullName ?? DBNull.Value),
                new SqlParameter("@Phone", (object)Phone ?? DBNull.Value),
                new SqlParameter("@DepartmentID", (object)DepartmentID ?? DBNull.Value)).ToList();

            if (giangviens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", giangviens);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var instructors = db.Instructors.Include(i => i.Department).Include(i => i.User);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View(instructors.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,UserID,FullName,Phone,DepartmentID")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", instructor.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", instructor.UserID);
            return View(instructor);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Include(i => i.User).FirstOrDefault(i => i.InstructorID == id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", instructor.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", instructor.UserID);
            return View(instructor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,UserID,FullName,Phone,DepartmentID")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(instructor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex) {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }
            var user = db.Users.Find(instructor.UserID);
            if (user != null)
            {
                instructor.User = db.Users.Find(instructor.UserID);
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", instructor.DepartmentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", instructor.UserID);
            return View(instructor);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
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
