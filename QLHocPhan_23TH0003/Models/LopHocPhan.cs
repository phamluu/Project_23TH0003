namespace QLHocPhan_23TH0003.Models
{
    public class LopHocPhan
    {
        public int Id { get; set; }
        public string? MaLopHocPhan { get; set; }
        public string? TenLopHocPhan { get; set; }
        public int? SiSoToiDa { get; set; }
       public string? ThoiGianHoc { get; set; }
        public string? DiaDiemHoc { get; set; }
        public string? GhiChu { get; set; }

        public decimal? HeSoChuyenCan { get; set; }
        public decimal? HeSoGiuaKy { get; set; }
        public decimal? HeSoThucHanh { get; set; }
        public decimal? HeSoCuoiKy { get; set; }


        public int IdHocPhan { get; set; }
        public HocPhan? HocPhan { get; set; }


        public ICollection<DangKyHocPhan>? DangKyHocPhans { get; set; }
        public ICollection<PhanCongGiangDay>? PhanCongGiangDays { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
