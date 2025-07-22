using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLHocPhan_23TH0003.Service
{
    public class UserService
    {
        private readonly MainDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(MainDbContext context, SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserViewModel GetUserInfo(string userId)
        {
            var model = new UserViewModel();
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                model.UserName = user.UserName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.EmailConfirmed = user.EmailConfirmed;
                model.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            }
            var giangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == userId);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);
            if (giangVien != null)
            {
                model.GiangVien = giangVien;
            }
            if (sinhVien != null)
            {
                model.SinhVien = sinhVien;
            }
            return model;
        }
        public GiangVien GetGiangVienInfo(string userId)
        {
            var giangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == userId);
            return giangVien;
        }

        public SinhVien GetSinhVienInfo(string userId)
        {
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == userId);
            return sinhVien;
        }
        public async Task<bool> UpdateGiangVien(GiangVien model)
        {
           var giangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == model.UserId);
            if (giangVien == null)
            {
                return false;
            }
            giangVien.HoTen = model.HoTen;
            giangVien.GioiTinh = model.GioiTinh;
            giangVien.NgaySinh = model.NgaySinh;
            giangVien.DiaChi = model.DiaChi;
            giangVien.HinhDaiDien = model.HinhDaiDien;
            _context.GiangVien.Update(giangVien);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSinhVien(SinhVien model)
        {
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == model.UserId);
            if (sinhVien == null)
            {
                return false;
            }
            sinhVien.HoTen = model.HoTen;
            sinhVien.GioiTinh = model.GioiTinh;
            sinhVien.NgaySinh = model.NgaySinh;
            sinhVien.DiaChi = model.DiaChi;
            sinhVien.HinhDaiDien = model.HinhDaiDien;
            _context.SinhVien.Update(sinhVien);
            await _context.SaveChangesAsync();
            return true;
        }

        #region Tài khoản
        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            return await _signInManager.UserManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        public async Task<IdentityResult> ChangeEmail(string userId, string newEmail)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            return await _signInManager.UserManager.SetEmailAsync(user, newEmail);
        }
        public async Task<IdentityResult> ChangePhoneNumber(string userId, string newPhoneNumber)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            return await _signInManager.UserManager.SetPhoneNumberAsync(user, newPhoneNumber);
        }
        
        public async Task<IdentityResult> ThemTaiKhoanAsync(string IdRole, string Email, string Password)
        {
            var user = new IdentityUser
            {
                UserName = Email,
                Email = Email,
            };

            var result = await _userManager.CreateAsync(user, Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(IdRole))
                {
                    await _roleManager.CreateAsync(new IdentityRole(IdRole));
                }

                await _userManager.AddToRoleAsync(user, IdRole);
            }
            return result;
        }
        public bool UpdateTaiKhoan(string UserId, string Email, string Password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
            if (user != null)
            {
                user.UserName = Email;
                user.Email = Email;
                if (Password != null) user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, Password);
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

    }
}

//user.Email = model.Email;
//if (!string.IsNullOrEmpty(model.Password))
//{
//    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
//    if (!result.Succeeded)
//    {
//        foreach (var error in result.Errors)
//        {
//            ModelState.AddModelError(string.Empty, error.Description);
//        }
//        return View("TaiKhoan", user); // Truyền lại model nếu tạo thất bại
//    }

//}
//await _userManager.UpdateAsync(user);
