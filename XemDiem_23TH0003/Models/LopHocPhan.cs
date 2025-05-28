namespace XemDiem_23TH0003.Models
{
    public class LopHocPhan
    {
        public int Id { get; set; }
        public string MaLopHocPhan { get; set; }
        public int IdHocPhan { get; set; }
        public HocPhan HocPhan { get; set; }

        public int IdGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }

        public ICollection<DangKyHocPhan> DangKyHocPhans { get; set; } 
    }
}
