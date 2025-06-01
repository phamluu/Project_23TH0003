namespace XemDiem_23TH0003.Models
{
    public class Khoa
    {
        public int Id { get; set; }
        public string? MaKhoa { get; set; }
        public string? TenKhoa { get; set; }

        public int? IdTruongKhoa { get; set; }
        public virtual GiangVien? TruongKhoa { get; set; }

        public virtual  ICollection<GiangVien>? GiangViens { get; set; }
        public virtual  ICollection<Lop>? Lops { get; set; }
        public virtual  ICollection<MonHoc>? MonHocs { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public DateTime? NgayCapNhat { get; set; }
    }
}
