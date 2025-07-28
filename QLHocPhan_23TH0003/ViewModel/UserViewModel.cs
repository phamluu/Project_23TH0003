using Microsoft.AspNetCore.Identity;
using QLHocPhan_23TH0003.Models;
using System.ComponentModel.DataAnnotations;


namespace QLHocPhan_23TH0003.ViewModel
{
    public class UserViewModel: IdentityUser
    {
        public List<string> Roles { get; set; }
        public bool IsDuyetHoSo { get; set; }
        public GiangVien GiangVien { get; set; }
        public SinhVien SinhVien { get; set; }
    }

    public class GiangVienViewModel:GiangVien
    {
        public IdentityUser User{ get; set; }
    }

    public class SinhVienViewModel : SinhVien
    {
        public IdentityUser User { get; set; }
    }

    public class TaiKhoanGiangVien
    {
        public int IdGiangVien { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TaiKhoanSinhVien
    {
        public int IdSinhVien { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }


}
