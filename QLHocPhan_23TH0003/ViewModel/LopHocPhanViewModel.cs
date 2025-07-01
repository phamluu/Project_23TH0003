using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class HocViewModel
    {
        public LopHocPhan? LopHocPhan { get; set; }
        public IEnumerable<BaiHocViewModel>? BaiHocs { get; set; }
    }

}
