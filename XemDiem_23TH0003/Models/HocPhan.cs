namespace XemDiem_23TH0003.Models
{
    public class HocPhan
    {
        public int Id { get; set; }
        public string? MaHP { get; set; }

        public int IdMonHoc { get; set; }
        public MonHoc MonHoc { get; set; }
        
        public int IdGiangVien { get; set; }
        public GiangVien GiangVien { get; set; }
        
        public string? TenHP { get; set; }
        public int SoTC { get; set; }
        public string ? HocKy { get; set; }
        public int NamHoc { get; set; }

        public ICollection<LopHocPhan> LopHocPhans { get; set; } 
    }
}
