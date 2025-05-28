namespace XemDiem_23TH0003.Models
{
    public class GiangVien
    {
        public int Id { get; set; }
        public string? MaGiangVien { get; set; }
        public string? HoTen { get; set; }
        public int IdKhoa { get; set; }
        
        public Khoa? Khoa { get; set; }
        public ICollection<HocPhan>? HocPhans { get; set; }
    }
}
