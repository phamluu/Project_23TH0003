using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class MonHocViewModel:MonHoc
    {
        public string TenKhoa { get; set; }
        public string Action { get; set; }
        public IEnumerable<Khoa> Khoas { get; set; }
    }
}
