using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
   
    public class DangKyHoc_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        #region Đăng ký học phần
        public ActionResult Index()
        {
            var ListClasses = db.Classes.ToList();
            var ListStudents = db.Enrollments.ToList();
            ViewBag.ListClasses = ListClasses;
            return View(ListStudents);
        }
        [Authorize(Roles = "admin, giangvien")]
        [HttpPost]
        public ActionResult List(int ClassID, List<int> StudentID)
        {
            var classs = db.Classes.Find(ClassID);
            if (classs == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại khóa học";
                return RedirectToAction("Index");
            }
            if (StudentID == null)
            {
                TempData["ErrorMessage"] = "Không có sinh viên đăng ký khóa học" + classs.Cours.CourseName;
                return RedirectToAction("Index");
            }
            foreach (var item in StudentID)
            {
                if (db.Enrollments.Any(e => e.ClassID == ClassID && e.StudentID == item)) continue;
                var enrollment = new Enrollment
                {
                    ClassID = ClassID,
                    StudentID = item
                };
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
            }
            TempData["SuccessMessage"] = "Đăng ký học phần " + classs.Cours.CourseName + " thành công.";
            return RedirectToAction("Index");
        }

        #endregion

        [Authorize(Roles = "admin, giangvien")]
        public ActionResult List(int ClassID)
        {
            var Classes = db.Classes.Find(ClassID);
            var enrollments = db.Enrollments.Include(e => e.Class).Include(e => e.Student).Where(e => e.ClassID == ClassID).Select(x => new EnrollmentViewModel
            {
                EnrollmentID = x.EnrollmentID,
                ClassID = x.ClassID,
                Class = x.Class,
                StudentID = x.StudentID,
                Student = x.Student,
                Midterm = x.Midterm,
                Final = x.Final
            });
            ViewBag.Classes = Classes;
            return View(enrollments);
        }
        [Authorize(Roles = "admin, giangvien")]
        [HttpPost]
        public ActionResult UpdateScores(int classID, List<Enrollment> ListEnrollments)
        {
            if (ListEnrollments != null)
            {
                foreach (var item in ListEnrollments)
                {
                    var enrollment = db.Enrollments.FirstOrDefault(e => e.EnrollmentID == item.EnrollmentID);

                    if (enrollment != null)
                    {
                        enrollment.Midterm = item.Midterm;
                        enrollment.Final = item.Final;
                        db.SaveChanges();
                    }
                }
            }
            
            return RedirectToAction("List", new { ClassID = classID });
        }
        
        

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName");
            return View();
        }

        // POST: DangKyHoc_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,StudentID,ClassID,Midterm,Final")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID", enrollment.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        [Authorize(Roles = "admin")]
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
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,StudentID,ClassID,Midterm,Final")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassID", enrollment.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            try
            {
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Xóa đăng ký học phẩn thành công!";
                return RedirectToAction("List", new { ClassID = enrollment.ClassID });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            TempData["ErrorMessage"] = "Không thể xóa đăng ký học phần";
            return RedirectToAction("List", new { ClassID = enrollment.ClassID });
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
