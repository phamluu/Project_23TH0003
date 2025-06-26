using System.ComponentModel.DataAnnotations;

namespace QLHocPhan_23TH0003.Enums
{
    public enum TrangThaiDangKy
    {
        [Display(Name = "Chờ duyệt")]
        ChoDuyet = 0,
        [Display(Name = "Đã đăng ký")]
        DaDangKy = 1,
        [Display(Name = "Đã hủy")]
        DaHuy = 2
    }
}
