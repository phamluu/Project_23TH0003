using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    [Authorize(Roles = "admin")]
    public class Users_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();
        [HttpGet]
        public ActionResult TimKiemNC(string RoleID = "", string Email = "", string Username = "")
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", RoleID);
            ViewBag.Email = Email;
            ViewBag.Username = Username;
            var users = db.Users.SqlQuery("EXEC User_TimKiem @RoleID,@Email,@Username",
                new SqlParameter("@RoleID", (object)RoleID ?? DBNull.Value),
                new SqlParameter("@Email", (object)Email ?? DBNull.Value),
                new SqlParameter("@Username", (object)Username ?? DBNull.Value)
                ).ToList();

            if (users.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View("Index", users);
        }
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View(users.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int error = 0;
                    var existingUser = db.Users.SingleOrDefault(x => x.Username == model.Username);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Username", "Tên người dùng đã tồn tại.");
                        error++;
                    }
                    var existingEmail = db.Users.SingleOrDefault(x => x.Email == model.Email);
                    if (existingEmail != null)
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                        error++;
                    }
                    if (error > 0)
                    {
                        ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", model.RoleID);
                        return View(model);
                    }
                    string hashpass = Crypto.HashPassword(model.Password);
                    var user = new User { Username = model.Username, Email = model.Email, RoleID = model.RoleID, Password = hashpass };
                    db.Users.Add(user);
                    db.SaveChanges();
                    if (model.RoleID == "giangvien")
                    {
                        var giangvien = new Instructor
                        {
                            UserID = user.UserID,
                        };
                        db.Instructors.Add(giangvien);
                    }
                    else if (model.RoleID == "sinhvien")
                    {
                        var sinhvien = new Student
                        {
                            UserID = user.UserID,
                        };
                        db.Students.Add(sinhvien);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", model.RoleID);
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View("Edit",user);
        }

        // POST: Users_23TH0003/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,RoleID,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int error = 0;
                    var existingUser = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                    // Kiểm tra nếu có tồn tại người dùng thì thực hiện tiếp
                    if (existingUser != null)
                    {
                        // Kiểm tra tồn tại Email
                        bool usernameExists = db.Users.Any(u => u.Username == user.Username && u.UserID != user.UserID);
                        if (usernameExists)
                        {
                            ModelState.AddModelError("Username", "Username đã tồn tại.");
                            error++;
                        }
                        bool emailExists = db.Users.Any(u => u.Email == user.Email && u.UserID != user.UserID);
                        if (emailExists)
                        {
                            ModelState.AddModelError("Email", "Email đã tồn tại.");
                            error++;
                        }
                        if (error > 0)
                        {
                            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
                            return View(user);
                        }
                        // Nếu có thay đổi mật khẩu
                        if (!string.IsNullOrEmpty(user.Password) && user.Password != existingUser.Password)
                        {
                            existingUser.Password = Crypto.HashPassword(user.Password);
                        }
                        existingUser.Username = user.Username;
                        existingUser.Email = user.Email;
                        string oldRoleID = existingUser.RoleID; // Lấy vai trò ban đầu
                        existingUser.RoleID = user.RoleID;
                        // Đánh dấu entity là đã thay đổi và lưu lại
                        db.Entry(existingUser).State = EntityState.Modified;
                        db.SaveChanges();

                        // TH thay đổi vai trò ==> Xóa thông tin vai trò cũ, thêm thông tin vai trò mới
                        if (oldRoleID != user.RoleID)
                        {
                            if (user.RoleID == "giangvien")
                            {
                                string FullName = "";
                                string Phone = "";
                                if (oldRoleID == "sinhvien")
                                {
                                    var sinhvien = db.Students.SingleOrDefault(x => x.UserID == user.UserID);
                                    if (sinhvien != null)
                                    {
                                        FullName = sinhvien.FullName;
                                        Phone = sinhvien.Phone;
                                        db.Students.Remove(sinhvien);
                                    }
                                }
                                db.Instructors.Add(new Instructor
                                {
                                    UserID = user.UserID,
                                    FullName = FullName,
                                    Phone = Phone
                                });
                            }
                            else if (user.RoleID == "sinhvien")
                            {
                                string FullName = "";
                                string Phone = "";
                                if (oldRoleID == "giangvien") {
                                    var giangvien = db.Instructors.SingleOrDefault(x => x.UserID == user.UserID);
                                    if(giangvien != null)
                                    {
                                        FullName = giangvien.FullName;
                                        Phone = giangvien.Phone;
                                        db.Instructors.Remove(giangvien);
                                    }
                                }
                                db.Students.Add(new Student
                                {
                                    UserID= user.UserID,
                                    FullName = FullName,
                                    Phone = Phone
                                });
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Users_23TH0003/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users_23TH0003/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
