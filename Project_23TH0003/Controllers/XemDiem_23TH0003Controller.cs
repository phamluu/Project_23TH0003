using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    public class XemDiem_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        [Authorize(Roles = "admin,sinhvien,giangvien")]
        public ActionResult Index()
        {
            var UserID = User.Identity.GetUserId();
            if (User.IsInRole("admin"))
            {
                var enrollments = db.Enrollments
                                    .Include(e => e.Class)
                                    .Include(e => e.Student).Select(e => new EnrollmentViewModel
                                    {
                                        ClassID = e.ClassID,
                                        StudentID = e.StudentID,
                                        Class = e.Class,
                                        Student = e.Student,
                                        Midterm = e.Midterm,
                                        Final = e.Final,
                                        EnrollmentID = e.EnrollmentID,
                                    });
                return View(enrollments.ToList());
            }
            else if (User.IsInRole("giangvien"))
            {
                var enrollments = db.Enrollments
                .Include(e => e.Class)
                .Include(e => e.Student)
                .Where(e => e.Class.Instructor.UserID.ToString() == UserID).
                Select(e => new EnrollmentViewModel
                {
                    ClassID = e.ClassID,
                    StudentID = e.StudentID,
                    Class = e.Class,
                    Student = e.Student,
                    Midterm = e.Midterm,
                    Final = e.Final,
                    EnrollmentID = e.EnrollmentID,
                });
                return View(enrollments.ToList());
            }
            if (User.IsInRole("sinhvien"))
            {
                var student = db.Students.SingleOrDefault(x => x.UserID.ToString() == UserID);
                if (student != null)
                {
                    var enrollments = db.Enrollments.Include(e => e.Class).Include(e => e.Student)
                        .Where(e => e.StudentID == student.StudentID).
                        Select(e => new EnrollmentViewModel
                        {
                            ClassID = e.ClassID,
                            StudentID = e.StudentID,
                            Class = e.Class,
                            Student = e.Student,
                            Midterm = e.Midterm,
                            Final = e.Final,
                            EnrollmentID = e.EnrollmentID,
                        });
                    return View(enrollments.ToList());
                }
            }

            return View();
        }

        // GET: XemDiem_23TH0003/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: XemDiem_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "UserID");
            return View();
        }

        // POST: XemDiem_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,StudentID,ClassID,Midterm,Final,CourseGrade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID", enrollment.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "UserID", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: XemDiem_23TH0003/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID", enrollment.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "UserID", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: XemDiem_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,StudentID,ClassID,Midterm,Final,CourseGrade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID", enrollment.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "UserID", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: XemDiem_23TH0003/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: XemDiem_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
