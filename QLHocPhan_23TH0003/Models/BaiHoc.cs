namespace QLHocPhan_23TH0003.Models
{
    public class BaiHoc
    {
        public int Id { get; set; }
        public string? TenBaiHoc { get; set; }
        public string? NoiDung { get; set; }
        public string? Video { get; set; }
        public string? TaiLieu { get; set; }
        public int? IdLopHocPhan { get; set; }
        public LopHocPhan? LopHocPhan { get; set; }
    }
}
