using System.Collections.ObjectModel;

namespace XemDiem_23TH0003.Models
{
    public class MonHoc
    {
        public int Id { get; set; }
        public string? MaMH { get; set; }
        public string? TenMH { get; set; }
        public int SoTC { get; set; }
        public Collection<HocPhan>? HocPhans { get; set; }
    }
}
