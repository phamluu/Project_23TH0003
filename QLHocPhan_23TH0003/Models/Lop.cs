namespace QLHocPhan_23TH0003.Models
{
    public class Lop
    {
        public int Id { get; set; }
        public string? MaLop { get; set; }
        public string? TenLop { get; set; }

        public int? IdKhoa { get; set; }
        public Khoa? Khoa { get; set; }

        public ICollection<SinhVien>? SinhViens { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
