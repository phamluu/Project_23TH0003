namespace XemDiem_23TH0003.Models
{
    public class HocPhan
    {
        public int Id { get; set; }
        public string? MaHocPhan { get; set; }
        public string? TenHocPhan { get; set; }

        public int IdHocKy { get; set; }
        public HocKy? HocKy { get; set; }
        public int? SoTinChi { get; set; }

        public int IdMonHoc { get; set; }
        public MonHoc? MonHoc { get; set; }
        
        public ICollection<LopHocPhan>? LopHocPhans { get; set; }

      
        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
