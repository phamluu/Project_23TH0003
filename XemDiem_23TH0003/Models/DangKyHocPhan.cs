namespace XemDiem_23TH0003.Models
{
    // Sinh viên đăng ký lớp học phần
    public class DangKyHocPhan
    {
        public int Id { get; set; }
        public int IdSinhVien { get; set; }
        public SinhVien SinhVien { get; set; }

        public int IdLopHocPhan { get; set; }
        public LopHocPhan LopHocPhan { get; set; }

        public Diem Diem { get; set; }
        
    }
}
