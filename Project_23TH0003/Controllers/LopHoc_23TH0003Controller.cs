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
    [Authorize(Roles = "admin,giangvien")]
    public class LopHoc_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        [HttpGet]
        public ActionResult TimKiemNC(int? CourseID = null, int? InstructorID = null, int? Semester = null, int? Year = null)
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName", InstructorID);
            ViewBag.Semester = Semester;
            ViewBag.Year = Year;

            var classs = db.Classes.SqlQuery("EXEC Class_TimKiem @CourseID, @InstructorID, @Semester, @Year",
                new SqlParameter("@CourseID", (object)CourseID ?? DBNull.Value),
                new SqlParameter("@InstructorID", (object)InstructorID ?? DBNull.Value),
                new SqlParameter("@Semester", (object)Semester ?? DBNull.Value),
                new SqlParameter("@Year", (object)Year ?? DBNull.Value)
                ).ToList();

            if (classs.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", classs);
        }
        public ActionResult Index()
        {
            List<Class> classes = new List<Class>();
            string UserID = User.Identity.GetUserId();
            if (User.IsInRole("admin"))
            {
                classes = db.Classes.Include(c => c.Cours).Include(c => c.Instructor).ToList();
                ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
                ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName");
            }
            else if (User.IsInRole("giangvien"))
            {
                 classes = db.Classes.Include(c => c.Cours).Include(c => c.Instructor)
                    .Where(c => c.Instructor.UserID.ToString() == UserID).ToList();

                var coures = db.Classes.Include(e => e.Cours).Include(e => e.Instructor)
                    .Where(e => e.Instructor.UserID.ToString() == UserID).Select(e => e.Cours).Distinct().ToList();
                ViewBag.CourseID = new SelectList(coures, "CourseID", "CourseName");

                var instructors = db.Instructors.Where(x => x.UserID.ToString() == UserID);
                ViewBag.InstructorID = new SelectList(instructors, "InstructorID", "FullName");
            }
            return View(classes.ToList());
        }

        // GET: LopHoc_23TH0003/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: LopHoc_23TH0003/Create
        private List<Instructor> GetInstructors(int CourseID)
        {
            var course = db.Courses.Find(CourseID);
            if (course != null)
            {
                var instructors = db.Instructors.Where(i => i.DepartmentID == course.DepartmentID).ToList();
                return instructors;
            }
            return new List<Instructor>();
        }
        public ActionResult Create()
        {
            string UserID = User.Identity.GetUserId();
            //if (User.IsInRole("giangvien"))
            //{
            //    var instructors = db.Instructors.Where(x => x.UserID.ToString() == UserID);
            //    ViewBag.InstructorID = new SelectList(instructors, "InstructorID", "FullName");
            //}
            //else
            //{
            //    ViewBag.InstructorID = new SelectList(db.Instructors, "InstructorID", "FullName");
            //}
            
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            var courseFirst = db.Courses.FirstOrDefault();
            var Instructor = new List <Instructor>();
            ViewBag.InstructorID = new SelectList(Instructor, "InstructorID", "FullName");
            return View();
        }

        // POST: LopHoc_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,CourseID,InstructorID,Semester,Year")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", @class.CourseID);
            var instructor = GetInstructors(@class.CourseID);
            ViewBag.InstructorID = new SelectList(instructor, "InstructorID", "FullName", @class.InstructorID);
            return View(@class);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            string UserID = User.Identity.GetUserId();
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            var instructor = GetInstructors(@class.CourseID);
            ViewBag.InstructorID = new SelectList(instructor, "InstructorID", "FullName", @class.InstructorID);

            return View(@class);
        }

        // POST: LopHoc_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,CourseID,InstructorID,Semester,Year")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", @class.CourseID);
            var instructor = GetInstructors(@class.CourseID);
            ViewBag.InstructorID = new SelectList(instructor, "InstructorID", "FullName", @class.InstructorID);
            return View(@class);
        }

        // GET: LopHoc_23TH0003/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: LopHoc_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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
