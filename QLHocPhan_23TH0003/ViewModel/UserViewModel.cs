using Microsoft.AspNetCore.Identity;
using QLHocPhan_23TH0003.Models;


namespace QLHocPhan_23TH0003.ViewModel
{
    public class UserViewModel: IdentityUser
    {
        public List<string> Roles { get; set; }
        public bool IsDuyetHoSo { get; set; }
        public GiangVien GiangVien { get; set; }
        public SinhVien SinhVien { get; set; }
    }
}
