namespace XemDiem_23TH0003.Models
{
    public class Diem
    {
        public int Id { get; set; }
        public float DiemChuyenCan { get; set; }
        public float DiemGiuaKy { get; set; }
        public float DiemCuoiKy { get; set; }

        public int IdDangKyHocPhan { get; set; }
        public DangKyHocPhan DangKyHocPhan { get; set; }

    }
}
