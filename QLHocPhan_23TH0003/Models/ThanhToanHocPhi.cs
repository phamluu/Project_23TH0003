using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHocPhan_23TH0003.Models
{
    public class ThanhToanHocPhi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdSinhVien { get; set; } 

        [Required]
        public int IdHocKy { get; set; } // VD: "Học kỳ 1"
        public virtual HocKy HocKy { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; } // Số tiền đã thanh toán

        public int PhuongThuc { get; set; } // Chuyển khoản, MoMo, ATM...
        public int TrangThai { get; set; }
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;

        public virtual SinhVien SinhVien { get; set; }
    }
}
