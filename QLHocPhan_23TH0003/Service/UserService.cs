using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Service
{
    public class UserService
    {
        private readonly MainDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(MainDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
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
    }
}
