namespace XemDiem_23TH0003.Models
{
    public class Khoa
    {
        public int Id { get; set; }
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }
        public ICollection<GiangVien>? GiangViens { get; set; }
        public ICollection<SinhVien>? SinhViens { get; set; }
        public ICollection<Lop>? Lops { get; set; }
    }
}
