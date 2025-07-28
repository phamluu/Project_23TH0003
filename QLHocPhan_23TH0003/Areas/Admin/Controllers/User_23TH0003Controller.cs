using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    // Cập nhật quản lý tài khoản của người dùng khác
    public class User_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;
        public User_23TH0003Controller(MainDbContext context, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            UserService userService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
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
            var giangVien = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsDeleted != true && x.UserId == id).FirstOrDefault();
            if (giangVien != null)
            {
                model.GiangVien = giangVien;
            }
            var sinhVien = _context.SinhVien.Include(x => x.Lop).Where(x => x.IsDeleted != true && x.UserId == id).FirstOrDefault();
            if (sinhVien != null)
            {
                model.SinhVien = sinhVien;
            }
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRoles(string email, List<string> selectedRoles)
        {
            var errorMessages = new List<string>();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Người dùng không tồn tại.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(selectedRoles).ToList();

            if (!rolesToAdd.Any() && !rolesToRemove.Any())
            {
                TempData["SuccessMessage"] = "Không có thay đổi nào được thực hiện.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // ====== Kiểm tra ràng buộc trước khi thực hiện thay đổi ======
                foreach (var role in rolesToRemove)
                {
                    switch (role)
                    {
                        case "GiangVien":
                            var messGv = await _userService.CanDeleteGiangVienAsync(user.Id);
                            errorMessages.AddRange(messGv);
                            break;

                        case "SinhVien":
                            var messSv = await _userService.CanDeleteSinhVienAsync(user.Id);
                            errorMessages.AddRange(messSv);
                            break;
                    }
                }

                if (errorMessages.Any())
                {
                    TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
                    return Redirect(Request.Headers["Referer"].ToString());
                }

                // ====== Thêm hồ sơ khi thêm vai trò ======
                foreach (var role in rolesToAdd)
                {
                    switch (role)
                    {
                        case "GiangVien":
                            var isGiangVienExists = await _context.GiangVien.AnyAsync(x => x.UserId == user.Id);
                            if (!isGiangVienExists)
                            {
                                _context.GiangVien.Add(new GiangVien
                                {
                                    UserId = user.Id,
                                    NgayTao = DateTime.Now
                                });
                            }
                            break;

                        case "SinhVien":
                            var isSinhVienExists = await _context.SinhVien.AnyAsync(x => x.UserId == user.Id);
                            if (!isSinhVienExists)
                            {
                                _context.SinhVien.Add(new SinhVien
                                {
                                    UserId = user.Id,
                                    NgayTao = DateTime.Now
                                });
                            }
                            break;
                    }
                }

                // ====== Xoá hồ sơ khi xoá vai trò ======
                foreach (var role in rolesToRemove)
                {
                    switch (role)
                    {
                        case "GiangVien":
                            var giangVienToRemove = await _context.GiangVien
                                .Where(x => x.UserId == user.Id)
                                .ToListAsync();
                            if (giangVienToRemove.Any())
                                _context.GiangVien.RemoveRange(giangVienToRemove);
                            break;

                        case "SinhVien":
                            var sinhVienToRemove = await _context.SinhVien
                                .Where(x => x.UserId == user.Id)
                                .ToListAsync();
                            if (sinhVienToRemove.Any())
                                _context.SinhVien.RemoveRange(sinhVienToRemove);
                            break;
                    }
                }

                // ====== Cập nhật vai trò ======
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                    throw new Exception("Thêm vai trò thất bại: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));

                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                    throw new Exception("Xoá vai trò thất bại: " + string.Join(", ", removeResult.Errors.Select(e => e.Description)));

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["SuccessMessage"] = "Cập nhật vai trò người dùng thành công.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                errorMessages.Add("Lỗi khi lưu vào cơ sở dữ liệu:");
                errorMessages.Add(ex.Message);
                var innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    errorMessages.Add($"- {innerEx.Message}");
                    innerEx = innerEx.InnerException;
                }

                TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        #region Tạo mật khẩu mới cho người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaoMatKhauMoi(string userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(newPassword))
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ.";
                return RedirectToAction("ChiTiet", new { id = userId });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng.";
                return RedirectToAction("Index");
            }

            // Xóa mật khẩu cũ (nếu có)
            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                TempData["ErrorMessage"] = "Không thể xóa mật khẩu cũ.";
                return RedirectToAction("Details", new { id = userId });
            }

            // Thêm mật khẩu mới
            var addResult = await _userManager.AddPasswordAsync(user, newPassword);
            if (addResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Tạo mật khẩu mới thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Tạo mật khẩu thất bại: " + string.Join(", ", addResult.Errors.Select(e => e.Description));
            }

            return RedirectToAction("Details", new { id = userId });
        }

        #endregion

    }
}
