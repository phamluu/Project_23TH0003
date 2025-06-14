using System.ComponentModel.DataAnnotations;

namespace QLHocPhan_23TH0003.Enums
{
    public enum Role
    {
        [Display(Name = "Giảng viên")]
        GiangVien,
        [Display(Name = "Sinh viên")]
        SinhVien
    }
}
