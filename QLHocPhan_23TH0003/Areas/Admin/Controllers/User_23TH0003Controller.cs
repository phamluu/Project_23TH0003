using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class User_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public User_23TH0003Controller(MainDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: User_23TH0003Controller
        public ActionResult Index()
        {
            // Load roles một lần
            var allRoles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();
            var model = _context.Users
                .AsEnumerable() // chuyển từ LINQ to Entities sang LINQ to Objects
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    EmailConfirmed = x.EmailConfirmed,
                    PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                    LockoutEnabled = x.LockoutEnabled,
                    LockoutEnd = x.LockoutEnd,
                    AccessFailedCount = x.AccessFailedCount,
                    TwoFactorEnabled = x.TwoFactorEnabled,
                    SecurityStamp = x.SecurityStamp,
                    ConcurrencyStamp = x.ConcurrencyStamp,
                    NormalizedUserName = x.NormalizedUserName,
                    NormalizedEmail = x.NormalizedEmail,
                    Roles = userRoles
                                .Where(ur => ur.UserId == x.Id)
                                .Join(allRoles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                                .ToList()
                })
                .ToList();

            return View(model);
        }

        // GET: User_23TH0003Controller/Details/5
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var user = _context.Users.Find(id);
            
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            ViewBag.UserRoles = userRoles;
            ViewBag.AllRoles = allRoles;


            var model = new UserViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.EmailConfirmed = user.EmailConfirmed;
            model.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            model.LockoutEnabled = user.LockoutEnabled;
            model.LockoutEnd = user.LockoutEnd;
            model.AccessFailedCount = user.AccessFailedCount;
            model.TwoFactorEnabled = user.TwoFactorEnabled;
            model.SecurityStamp = user.SecurityStamp;
            model.ConcurrencyStamp = user.ConcurrencyStamp;
            model.NormalizedUserName = user.NormalizedUserName;
            model.NormalizedEmail = user.NormalizedEmail;
            model.EmailConfirmed = user.EmailConfirmed;
            model.Roles = _context.UserRoles.Where(ur => ur.UserId == id)
                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name).ToList();
            return View(model);
        }

        // GET: User_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: User_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Học phần không tồn tại";
                return RedirectToAction("Index");
            }
            try
            {
                var hsGiangVien = _context.GiangVien.Any(hs => hs.UserId == id);
                var hsSinhVien = _context.SinhVien.Any(hs => hs.UserId == id);
                if (hsGiangVien)
                {
                    TempData["ErrorMessage"] = "Tồn tại hồ sơ giảng viên. Vui lòng xóa hồ sơ giảng viên";
                    return RedirectToAction("Index");
                }
                if (hsSinhVien)
                {
                    TempData["ErrorMessage"] = "Tồn tại hồ sơ sinh viên. Vui lòng xóa hồ sơ sinh viên";
                    return RedirectToAction("Index");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa tài khoản người dùng thành công";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra";
            }
            // Xóa hồ sơ giảng viên, sinh viên
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(string email, List<string> selectedRoles)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Người dùng không tồn tại.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(selectedRoles).ToList();
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                    return BadRequest("Thêm vai trò thất bại.");
            }
            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                    return BadRequest("Xoá vai trò thất bại.");
            }
            TempData["SuccessMessage"] = "Cập nhật vai trò người dùng thành công.";
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
