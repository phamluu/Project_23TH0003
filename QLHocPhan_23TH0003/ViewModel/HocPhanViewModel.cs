using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class HocPhanViewModel:HocPhan
    {
        public string TenMonHoc { get; set; }
        public string Action { get; set; }
        public string TenHocKy { get; set; }

        public IEnumerable<MonHoc> MonHocs { get; set; }
        public IEnumerable<HocKy> HocKies { get; set; }
    }
}
