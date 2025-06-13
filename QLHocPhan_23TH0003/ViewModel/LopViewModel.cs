using Microsoft.AspNetCore.Mvc.Rendering;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class LopViewModel:Lop
    {
        public IEnumerable<Khoa> KhoaList { get; set; }
        public string TenKhoa { get; set; }
        public string Action { get; set; }
    }
}
