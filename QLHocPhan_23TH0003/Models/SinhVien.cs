namespace QLHocPhan_23TH0003.Models
{
    public class SinhVien
    {
        public int Id {  get; set; }
        public string? MaSinhVien { get; set; }

        // Thông tin profile
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? DiaChi { get; set; }
        // Thông tin profile

        public string UserId { get; set; }

        public int? IdLop { get; set; }
        public Lop? Lop { get; set; }

        public ICollection<DangKyHocPhan>? DangKyHocPhans { get; set; }
        public ICollection<ThanhToanHocPhi>? ThanhToanHocPhis { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
