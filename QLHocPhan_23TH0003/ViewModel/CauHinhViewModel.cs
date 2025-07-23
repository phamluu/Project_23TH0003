using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class CauHinhViewModel:CauHinhHeThong
    {
        public string? Action { get; set; }
        public LoaiCauHinh LoaiCauHinh { get; set; }
        public IFormFile? HinhDaiDienFile { get; set; }
    }
    
}
