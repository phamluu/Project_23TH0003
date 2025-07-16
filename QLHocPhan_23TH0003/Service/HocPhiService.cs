using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Service
{
    public class HocPhiService
    {
        private readonly QuanLyHocPhanDbContext _context;
        public HocPhiService(QuanLyHocPhanDbContext context)
        {
            _context = context;
        }
        // Lấy danh sách học phí theo học kì
        public List<HocPhiHocKyViewModel> GetHocPhiHocKy(int IdSinhVien)
        {
            // Truy vấn trước danh sách thanh toán để tránh N+1
            var thanhToans = _context.ThanhToanHocPhi
                .Where(x => x.IdSinhVien == IdSinhVien && x.TrangThai == (int)TrangThaiThanhToan.DaThanhToan)
                .GroupBy(x => x.IdHocKy)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.SoTien));

            // Truy vấn chính học phí theo từng học kỳ
            var result = _context.DangKyHocPhan
                .Include(x => x.LopHocPhan)
                    .ThenInclude(x => x.HocPhan)
                .Where(x => x.IdSinhVien == IdSinhVien && x.TrangThai == (int)TrangThaiDangKy.DaDangKy)
                .GroupBy(x => x.LopHocPhan.HocPhan.IdHocKy)
                .Select(g => new HocPhiHocKyViewModel
                {
                    HocKy = g.Key,
                    ChiTietHocPhi = g.Select(x => new HocPhiViewModel
                    {
                        LopHocPhan = x.LopHocPhan
                    }).ToList(),
                    DaDong = thanhToans.ContainsKey(g.Key) ? thanhToans[g.Key] : 0
                })
                .ToList();

            return result;
        }


        public HocPhiHocKyViewModel GetHocPhi(int IdSinhVien, int? IdHocKy)
        {
            HocPhiHocKyViewModel model = new HocPhiHocKyViewModel();
            var hocphi = _context.DangKyHocPhan
                .Include(x => x.LopHocPhan)
                    .ThenInclude(x => x.HocPhan)
                .Where(x => x.IdSinhVien == IdSinhVien && x.TrangThai == (int)TrangThaiDangKy.DaDangKy
                        && x.LopHocPhan.HocPhan.IdHocKy == IdHocKy)
                .Select(x => new HocPhiViewModel
                {
                    LopHocPhan = x.LopHocPhan
                })
            .ToList();
            model.ChiTietHocPhi = hocphi;
            model.HocKy = IdHocKy;
            return model;
        }
    }
}
