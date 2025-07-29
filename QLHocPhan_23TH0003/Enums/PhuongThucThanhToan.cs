using System.ComponentModel.DataAnnotations;

namespace QLHocPhan_23TH0003.Enums
{
    public enum PhuongThucThanhToan
    {
        [Display(Name = "Vietqr")]
        vietqr = 1,
        [Display(Name = "Zalopay")]
        zalopay = 2,
        [Display(Name = "VNPay")]
        vnpay = 3,
    }
}
