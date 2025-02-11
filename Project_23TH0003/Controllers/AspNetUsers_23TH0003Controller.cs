﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;

namespace Project_23TH0003.Controllers
{
    public class AspNetUsers_23TH0003Controller : Controller
    {
        private Project_23TH0003Entities db = new Project_23TH0003Entities();
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AspNetUsers_23TH0003Controller()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        public ActionResult AssignRole(string userId)
        {
            var user = _userManager.FindById(userId);
            var roles = _roleManager.Roles.ToList();
            var assignedRoles = _userManager.GetRoles(userId);

            var model = new AssignRoleViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                Roles = roles.Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name,
                    Selected = assignedRoles.Contains(role.Name)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole(AssignRoleViewModel model)
        {
            var user = _userManager.FindById(model.UserId);
            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                if (model.SelectedRoles.Contains(role.Name))
                {
                    if (!_userManager.IsInRole(user.Id, role.Name))
                    {
                        _userManager.AddToRole(user.Id, role.Name);
                        if (role.Name == "giangvien")
                        {
                            var giangvien = new Instructor
                            {
                                UserID = user.Id,
                            };
                            db.Instructors.Add(giangvien);
                        }
                        if (role.Name == "sinhvien")
                        {
                            var sinhvien = new Student
                            {
                                UserID = user.Id,
                            };
                            db.Students.Add(sinhvien);
                        }
                    }
                }
                else
                {
                    if (_userManager.IsInRole(user.Id, role.Name))
                    {
                        _userManager.RemoveFromRole(user.Id, role.Name);
                        // Xóa thông tin từ table có role tương ứng
                        if (role.Name == "giangvien")
                        {
                            db.Instructors.RemoveRange(db.Instructors.Where(x => x.UserID == user.Id));
                        }
                        if (role.Name == "sinhvien")
                        {
                            db.Students.RemoveRange(db.Students.Where(x => x.UserID == user.Id));
                        }
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: AspNetUsers_23TH0024
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            var model = users.Select(user => new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = (List<string>)_userManager.GetRoles(user.Id)
            }).ToList();

            return View(model);
        }

        // GET: AspNetUsers_23TH0024/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers_23TH0024/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers_23TH0024/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserName,Email,PasswordHash, PhoneNumber")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = aspNetUser.UserName, Email = aspNetUser.Email, PhoneNumber = aspNetUser.PhoneNumber };
                var result = await _userManager.CreateAsync(user, aspNetUser.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers_23TH0024/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers_23TH0024/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Email, PhoneNumber")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(aspNetUser.Id);
                user.UserName = aspNetUser.UserName;
                user.Email = aspNetUser.Email;
                user.PhoneNumber = aspNetUser.PhoneNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers_23TH0024/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers_23TH0024/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
