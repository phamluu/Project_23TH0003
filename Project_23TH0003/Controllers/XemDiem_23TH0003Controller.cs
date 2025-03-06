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

        // Xem bảng điêm tất cả các môn của tất cả sinh viên
        public ActionResult Index()
        {
            var allClasses = db.Classes.ToList();
            var allEnrollments = db.Enrollments.ToList();
            var groupedClassesByYear = allClasses.GroupBy(x => x.Year).ToList();
            List<XemDiemTheoNamHoc> ListNamHoc = new List<XemDiemTheoNamHoc>();

            foreach (var nam in groupedClassesByYear)
            {
                var _diemnamhoc = new XemDiemTheoNamHoc
                {
                    Year = nam.Key,
                    LisDiemHocKy = new List<XemDiemTheoHocKy>()
                };

                // Group các lớp theo học kỳ trong mỗi năm.
                var groupedClassesBySemester = nam.GroupBy(x => new { x.Semester, x.Year }).ToList();

                foreach (var hk in groupedClassesBySemester)
                {
                    var _diemHocKy = new XemDiemTheoHocKy
                    {
                        semester = hk.Key.Semester,
                        ListClass = hk.ToList(),
                        ListDiemSinhVien = new List<XemDiemSinhVien>()
                    };

                    // Lấy danh sách sinh viên đã đăng ký các lớp trong học kỳ và năm học cụ thể
                    var lstSinhVien = allEnrollments
                        .Where(x => x.Class.Year == hk.Key.Year && x.Class.Semester == hk.Key.Semester)
                        .Select(x => x.Student)
                        .Distinct()
                        .ToList();

                    foreach (var sinhvien in lstSinhVien)
                    {
                        var diemSinhVien = new XemDiemSinhVien
                        {
                            student = sinhvien,
                            ListDiem = allEnrollments
                                .Where(x => x.StudentID == sinhvien.StudentID && x.Class.Year == hk.Key.Year && x.Class.Semester == hk.Key.Semester)
                                .Select(x => new DiemViewModel { Midterm = x.Midterm, Final = x.Final, Class = x.Class })
                                .ToList()
                        };
                        _diemHocKy.ListDiemSinhVien.Add(diemSinhVien);
                    }

                    // Thêm học kỳ vào danh sách các học kỳ của năm học.
                    _diemnamhoc.LisDiemHocKy.Add(_diemHocKy);
                }

                // Thêm năm học vào danh sách kết quả.
                ListNamHoc.Add(_diemnamhoc);
            }

            return View(ListNamHoc);
        }

        public ActionResult NhapDiem()
        {
            var enrollments = db.Enrollments.Select(e => new EnrollmentViewModel
            {
                EnrollmentID = e.EnrollmentID,
                ClassID = e.ClassID,
                Class = e.Class,
                StudentID = e.StudentID,
                Student = e.Student,
                Midterm = e.Midterm,
                Final = e.Final
            });
            return View(enrollments);
        }
        [HttpPost]
        public ActionResult UpdateScores(int classID, List<Enrollment> ListEnrollments)
        {
            var classes = db.Classes.SingleOrDefault(c => c.ClassID == classID);
            if (classes == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại khóa học";
                return RedirectToAction("NhapDiem");
            }
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
                TempData["SuccessMessage"] = "Cập nhật điểm " + classes.Cours.CourseName + " thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Cập nhật điểm " + classes.Cours.CourseName + " thất bại.";
            }

            return RedirectToAction("NhapDiem");
        }

        [Authorize(Roles = "admin,sinhvien,giangvien")]
        public ActionResult XemDiemTheoMon()
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

        // Xem bảng điểm chi tiết của 1 sinh viên
        public ActionResult XemDiemSinhVien(int studentId)
        {
            var student = db.Students.SingleOrDefault(x => x.StudentID == studentId);
            if (student == null)
            {
                return View();
            }
                XemDiemTongKet tongket = new XemDiemTongKet();
                tongket.StudentId = studentId;
                var groupedEnrollments = student.Enrollments.GroupBy(x => new { x.Class.Semester, x.Class.Year }).ToList();

                tongket.ListDiemHocKy = groupedEnrollments
                .Select(g => new XemDiemSinhVienTheoHocKy
                {
                    HocKyNamHoc = $"Học kỳ {g.Key.Semester} - Năm học {g.Key.Year}-{g.Key.Year + 1}",
                    ListDiem = g.Select(e => new DiemViewModel
                    {
                        Midterm = e.Midterm,
                        Final = e.Final,
                        Class = e.Class
                    }).ToList()
                }).ToList();
            return View(tongket);
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
