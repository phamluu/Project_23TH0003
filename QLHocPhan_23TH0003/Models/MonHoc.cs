using System.Collections.ObjectModel;

namespace QLHocPhan_23TH0003.Models
{
    public class MonHoc
    {
        public int Id { get; set; }
        public string? MaMonHoc { get; set; }
        public string? TenMonHoc { get; set; }

        public int? IdKhoa { get; set; }
        public Khoa? Khoa { get; set; }

        public Collection<HocPhan>? HocPhans { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
