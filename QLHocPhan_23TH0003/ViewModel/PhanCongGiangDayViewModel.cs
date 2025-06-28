using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class PhanCongGiangDayViewModel
    {
        public int? IdLopHocPhan { get; set; }
        public List<LopHocPhan>? LopHocPhans { get; set; } 
        public List<GiangVien>? GiangViens { get; set; }
        public List<int> GiangVienDaPhanCong { get; set; }
    }
}
