using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Service
{
    public class CauHinhService
    {
        private readonly MainDbContext _context;

        public CauHinhService(MainDbContext context)
        {
            _context = context;
        }

        public string? GetGiaTri(string maCauHinh)
        {
            return _context.CauHinhHeThong
                .AsNoTracking()
                .FirstOrDefault(x => x.MaCauHinh == maCauHinh)?.GiaTri;
        }

        public Dictionary<string, string?> GetGiaTriNhieu(IEnumerable<string> maCauHinhs)
        {
            return _context.CauHinhHeThong
                .AsNoTracking()
                .Where(x => maCauHinhs.Contains(x.MaCauHinh))
                .ToDictionary(x => x.MaCauHinh, x => x.GiaTri);
        }

        public List<CauHinhViewModel> GetTatCa()
        {
            var ch = CauHinhHelper.GetCauHinhCoBan();
            var giaTriTuDb = _context.CauHinhHeThong.ToList();

            foreach (var item in ch)
            {
                var matching = giaTriTuDb.FirstOrDefault(x => x.MaCauHinh == item.MaCauHinh);
                if (matching != null)
                {
                    item.GiaTri = matching.GiaTri;
                    item.Id = matching.Id;
                }
            }
            return ch;
        }
    }
}
