using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Models;

namespace QLHocPhan_23TH0003.ViewModel
{
    public class HocPhiTongHopViewModel
    {
        public List<HocPhiHocKyViewModel> HocPhiHocKy { get; set; } = new(); // danh sách học phí học ký>
        public List<HocKy> HocKy { get; set; } 

    }
    public class HocPhiHocKyViewModel
    {
        public List<HocPhiViewModel> ChiTietHocPhi { get; set; } = new();

        public decimal TongHocPhi => ChiTietHocPhi.Sum(x => x.HocPhi); // Tổng học phí của học kỳ
        public decimal DaDong { get; set; } // tổng số tiền đã nộp trong kỳ

        public decimal NoCu { get; set; } // tổng nợ các kỳ trước

        public decimal ConNo => (TongHocPhi + NoCu) - DaDong;

        public string? NamHoc { get; set; }

        public int? HocKy { get; set; }

        public string? PhuongThuc { get; set; }
    }
    public class HocPhiViewModel:DangKyHocPhan
    {
        
        public decimal DonGia {
            get
            {
                var giaStr = CauHinhHelper.Get("HocPhi");
                if (decimal.TryParse(giaStr, out decimal gia))
                {
                    return gia;
                }
                return 0;
            }
        }
        public decimal HocPhi
        {
            get
            {
                return DonGia * (this.LopHocPhan?.HocPhan?.SoTinChi ?? 0);
            }
        }
    }
}
