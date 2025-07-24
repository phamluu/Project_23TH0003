using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Service
{
    public class BaoCaoService
    {
        private readonly MainDbContext _context;
        public BaoCaoService(MainDbContext context)
        {
            _context = context;
        }
        public IEnumerable<BaoCaoSinhVienViewModel> GetBaoCaoSinhVien()
        {
            var model = _context.Lop
                .Join(_context.Khoa,
                      lop => lop.IdKhoa,
                      khoa => khoa.Id,
                      (lop, khoa) => new { lop, khoa })
                .GroupJoin(_context.SinhVien,
                           lk => lk.lop.Id,
                           sv => sv.IdLop,
                           (lk, svGroup) => new BaoCaoSinhVienViewModel
                           {
                               MaLop = lk.lop.MaLop,
                               TenLop = lk.lop.TenLop,
                               Khoa = lk.khoa.TenKhoa,
                               SLNam = svGroup.Count(sv => sv.GioiTinh == (int)GioiTinh.Nam),
                               SLNu = svGroup.Count(sv => sv.GioiTinh == (int)GioiTinh.Nu),
                               TongSV = svGroup.Count()
                           });

            return model.ToList();
        }

        public IEnumerable<BaoCaoHocPhanViewModel> GetBaoCaoHocPhan()
        {
            var model = (from hp in _context.HocPhan.Include(x => x.HocKy).Include(x => x.MonHoc).ThenInclude(x => x.Khoa)
                         select new BaoCaoHocPhanViewModel
                         {
                             MaHocPhan = hp.MaHocPhan,
                             TenHocPhan = hp.TenHocPhan,
                             SoTinChi = hp.SoTinChi,
                             TenKhoa = hp.MonHoc.Khoa.TenKhoa,
                             HocKy = hp.HocKy.TenHocKy,
                             SoLopMo = hp.LopHocPhans.Count(),
                             NamHoc = hp.HocKy.NamHoc,
                             TongSV = _context.DangKyHocPhan.Where(x => x.LopHocPhan.IdHocPhan == hp.Id).Count()
                         });
            return model.ToList(); 
        }

        //public IEnumerable<BaoCaoHocPhiViewModel> GetBaoCaoHocPhi()
        //{
        //    var model = (from hp in _context.SinhVien
        //                 select new BaoCaoHocPhiViewModel
        //                 {
        //                     MaSV = hp.UserId,
        //                     HoTen = hp.HoTen,
        //                     TenLop = hp.Lop.TenLop,
        //                     HocKy = "",
        //                     NamHoc = "",
        //                     TongHocPhi = _context.DangKyHocPhan.Where(x => x.IdSinhVien == hp.Id).Sum(x => x.LopHocPhan.HocPhan.SoTinChi * 100000)
        //                 });
        //    return model.ToList();
        //}
        
    }
}
