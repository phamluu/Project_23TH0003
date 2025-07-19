using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using QLHocPhan_23TH0003.Data;
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
    }
}
