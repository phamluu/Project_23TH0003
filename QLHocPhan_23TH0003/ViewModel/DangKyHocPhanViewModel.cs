using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class DangKyHocPhanViewModel
    {
        public int? IdLopHocPhan { get; set; }
        public List<LopHocPhan> LopHocPhans { get; set; }
        public List<SinhVien> SinhViens { get; set; } // Sinh viên chưa đăng ký hoc phân
        public List<int> SinhVienDaDangKy { get; set; }
    }
}
