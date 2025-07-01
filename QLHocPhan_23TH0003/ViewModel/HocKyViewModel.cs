using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class HocKyViewModel:HocKy
    {
        public string Action { get; set; }
    }

    public class HocKyLopHocPhanViewModel
    {
        public HocKy HocKy { get; set; }
        public List<KhoaLopHocPhanViewModel> Khoas { get; set; }
    }

    public class KhoaLopHocPhanViewModel
    {
        public Khoa Khoa { get; set; }
        public IEnumerable<LopHocPhan> LopHocPhans { get; set; }
    }
}
