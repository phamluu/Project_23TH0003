namespace XemDiem_23TH0003.Models
{
    public class Diem
    {
        public int Id { get; set; }
        public decimal? DiemChuyenCan { get; set; }
        public decimal? DiemGiuaKy { get; set; }
        public decimal? DiemThucHanh { get; set; }
        public decimal? DiemCuoiKy { get; set; }

        public int IdDangKyHocPhan { get; set; }
        public DangKyHocPhan DangKyHocPhan { get; set; }
        
        public decimal? DiemTrungBinh { get; set; }
        public string? XepLoai { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }

    }
}
