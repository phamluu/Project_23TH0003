namespace XemDiem_23TH0003.Models
{
    public class SinhVien
    {
        public int Id {  get; set; }
        public string? MaSinhVien { get; set; }
        public string? HoTen { get; set; }
        public int? IdLop { get; set; }
        public int IdKhoa { get; set; }
        public DateTime NgaySinh { get; set; }
        public Khoa Khoa { get; set; }
        public Lop? Lop { get; set; }
        public ICollection<DangKyHocPhan>? DangKyHocPhan { get; set; }
    }
}
