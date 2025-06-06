namespace QLHocPhan_23TH0003.Models
{
    public class HocKy
    {
        public int Id { get; set; }
        // Ex: HK1_2025
        public string? MaHocKy { get; set; }
        //Ex: Học kỳ 1 năm học 2024-2025
        public string? TenHocKy { get; set; }
        // Ex: 2024-2025
        public string? NamHoc { get; set; }
        // Ex: 1
        public int ThuTu { get; set; }

        public ICollection<HocPhan>? HocPhans { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
