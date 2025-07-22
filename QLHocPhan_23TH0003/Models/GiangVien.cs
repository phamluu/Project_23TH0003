namespace QLHocPhan_23TH0003.Models
{
    public class GiangVien
    {
        public int Id { get; set; }
        public string? MaGiangVien { get; set; }

        // Thông tin profile
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? HinhDaiDien { get; set; }
        public string? DiaChi { get; set; }
        

        // end thông tin profile
        public string? UserId { get; set; }

        public int? IdKhoa { get; set; }
        public Khoa? Khoa { get; set; }
        public bool IsActive { get; set; }
        public ICollection<PhanCongGiangDay>? PhanCongGiangDays { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
