namespace XemDiem_23TH0003.Models
{
    public class PhanCongGiangDay
    {
        public int Id { get; set; }
        public int IdLopHocPhan { get; set; }
        public LopHocPhan LopHocPhan { get; set; }

        public int IdGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }

        public DateTime NgayPhanCong { get; set; }

        public string VaiTro { get; set; } // Chính, trợ giảng,..
    }
}
