using System.ComponentModel.DataAnnotations;

namespace QLHocPhan_23TH0003.Enums
{
    public enum VaiTroGiangVien
    {
        [Display(Name = "Giảng viên chính")]
        GiangVienChinh = 0,
        [Display(Name = "Trợ giảng")]
        TroGiang = 1,
        [Display(Name = "Hướng dẫn thực hành")]
        HuongDanThucHanh = 2
    }
}
