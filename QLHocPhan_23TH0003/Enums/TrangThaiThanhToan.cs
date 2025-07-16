using System.ComponentModel.DataAnnotations;

namespace QLHocPhan_23TH0003.Enums
{
    public enum TrangThaiThanhToan
    {
        [Display(Name = "Đã thanh toán")]
        DaThanhToan = 1,
        [Display(Name = "Đang xử lý")]
        DamgXuLy = 2,
        [Display(Name = "Đã hoàn tiền")]
        DaHoanTien = 3,
    }
}
