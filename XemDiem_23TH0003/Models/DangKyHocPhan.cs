using System.ComponentModel.DataAnnotations.Schema;
using XemDiem_23TH0003.Enums;

namespace XemDiem_23TH0003.Models
{
   
    // Sinh viên đăng ký lớp học phần
    public class DangKyHocPhan
    {
        public int Id { get; set; }
        public int IdSinhVien { get; set; }
        public SinhVien SinhVien { get; set; }

        public int IdLopHocPhan { get; set; }
        public LopHocPhan LopHocPhan { get; set; }

       // ví dụ: 0 = Chờ duyệt, 1 = Đã đăng ký, 2 = Đã hủy
        public int TrangThai { get; set; }
        [NotMapped]
        public TrangThaiDangKy TrangThaiEnum => (TrangThaiDangKy)TrangThai;

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }

    }
}
