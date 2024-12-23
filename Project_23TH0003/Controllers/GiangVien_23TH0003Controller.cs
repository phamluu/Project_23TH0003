using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    public class GiangVien_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();

        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            var instructors = db.Instructors.Include(i => i.Department).Include(i => i.User);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View(instructors.ToList());
        }

        // GET: GiangVien_23TH0003/Details/5
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

        // GET: GiangVien_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: GiangVien_23TH0003/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,UserID,FullName,Email,Phone,DepartmentID")] Instructor instructor)
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

        // GET: GiangVien_23TH0003/Edit/5
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

        // POST: GiangVien_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorID,UserID,FullName,Email,Phone,DepartmentID")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool emailExists = db.Instructors.Any(u => u.Email == instructor.Email && u.InstructorID != instructor.InstructorID);
                    if (!emailExists)
                    {
                        db.Entry(instructor).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("Email", "Email đã tồn tại");
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

        // GET: GiangVien_23TH0003/Delete/5
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

        // POST: GiangVien_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
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
