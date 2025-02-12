using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    public class SinhVien_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        public ActionResult GetStudents()
        {
            var students = db.Students.ToList();
            return PartialView("_StudentList", students); 
        }

        [Authorize(Roles = "sinhvien")]
        public ActionResult Profile()
        {
            var UserID = User.Identity.GetUserId();
            var student = db.Students.SingleOrDefault(x => x.UserID.ToString() == UserID);
            if (student == null)
            {
                return HttpNotFound();
            }
            var user = db.AspNetUsers.Where(d => d.Id == student.UserID).ToList();
            ViewBag.UserID = new SelectList(user, "UserID", "UserName", int.Parse(UserID));
            return View(student);
        }
        [Authorize(Roles = "sinhvien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "StudentID,UserID,FullName,DateOfBirth,Gender,Phone,Address")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            var user = db.AspNetUsers.Where(d => d.Id == student.UserID).ToList();
            ViewBag.UserID = new SelectList(user, "UserID", "UserName", student.UserID);
            return View(student);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TimKiemNC(string FullName = "", string Phone = null, string Address = null, 
            bool? Gender = null, int? DateOfBirthFrom = null, int? DateOfBirthTo = null)
        {
            ViewBag.FullName = FullName;
            ViewBag.Phone = Phone;
            ViewBag.Address = Address;
            ViewBag.Gender = Gender;
            ViewBag.DateOfBirthFrom = DateOfBirthFrom;
            ViewBag.DateOfBirthTo = DateOfBirthTo;

            var sinhviens = db.Students.SqlQuery("EXEC Student_TimKiem @FullName, @Phone, @Address, @Gender, @DateOfBirthFrom, @DateOfBirthTo", 
                new SqlParameter("@FullName", (object)FullName ?? DBNull.Value),
                new SqlParameter("@Phone", (object)Phone ?? DBNull.Value),
                new SqlParameter("@Address", (object)Address ?? DBNull.Value),
                new SqlParameter("@Gender", (object)Gender ?? DBNull.Value),
                new SqlParameter("@DateOfBirthFrom", (object)DateOfBirthFrom ?? DBNull.Value),
                new SqlParameter("@DateOfBirthTo", (object)DateOfBirthTo ?? DBNull.Value)
                ).ToList();

            if (sinhviens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", sinhviens);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.AspNetUser);
            return View(students.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,UserID,FullName,DateOfBirth,Gender,Phone,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", student.UserID);
            return View(student);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "UserID", "UserName", student.UserID);
            return View(student);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,UserID,FullName,DateOfBirth,Gender,Phone,Address")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                        db.Entry(student).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex) {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "UserID", "UserName", student.UserID);
            return View(student);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
