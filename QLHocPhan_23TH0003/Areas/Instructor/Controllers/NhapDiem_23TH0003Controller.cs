using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Instructor.Controllers
{
    public class NhapDiem_23TH0003Controller : BaseInstructorController
    {
        private readonly QLHocPhan_23TH0003.Data.MainDbContext _context;
        public NhapDiem_23TH0003Controller(QLHocPhan_23TH0003.Data.MainDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(int? IdLopHocPhan = null)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var giangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == userId);
            var phanCong = await _context.PhanCongGiangDay
                .Where(p => p.IdGiangVien == giangVien.Id)
                .Include(p => p.LopHocPhan)
                .ToListAsync();

            ViewBag.ListLopHocPhan = phanCong.Select(p => p.LopHocPhan).Distinct().ToList();

            if (IdLopHocPhan == null)
            {
                return View(null);
            }

            var danhSachDangKy = await _context.DangKyHocPhan
                .Where(dk => dk.IdLopHocPhan == IdLopHocPhan && dk.TrangThai == (int)TrangThaiDangKy.DaDangKy)
                .Include(dk => dk.SinhVien)
                .Include(dk => dk.Diem)
                .ToListAsync();

            ViewBag.LopHocPhanId = IdLopHocPhan;
            return View(danhSachDangKy);
        }

        private decimal? TinhDiemTrungBinh(Diem diem)
        {
            if (diem.DiemChuyenCan == null || diem.DiemGiuaKy == null || diem.DiemThucHanh == null || diem.DiemCuoiKy == null)
                return null;

            return Math.Round(
                (diem.DiemChuyenCan.Value * 0.1m) +
                (diem.DiemGiuaKy.Value * 0.2m) +
                (diem.DiemThucHanh.Value * 0.2m) +
                (diem.DiemCuoiKy.Value * 0.5m),
                2
            );
        }

        private string? TinhXepLoai(decimal? diemTB)
        {
            if (diemTB == null) return null;
            if (diemTB >= 8.5m) return "Giỏi";
            if (diemTB >= 7.0m) return "Khá";
            if (diemTB >= 5.0m) return "Trung bình";
            return "Yếu";
        }


        [HttpPost]
        public async Task<IActionResult> LuuDiem(int[] dangKyIds, decimal?[] diemChuyenCan,
            decimal?[] diemGiuaKy, decimal?[] diemThucHanh, decimal?[] diemCuoiKy)
        {
            for (int i = 0; i < dangKyIds.Length; i++)
            {
                var dkId = dangKyIds[i];
                var diem = await _context.Diem.FirstOrDefaultAsync(d => d.IdDangKyHocPhan == dkId);

                if (diem == null)
                {
                    diem = new Diem
                    {
                        IdDangKyHocPhan = dkId
                    };
                    _context.Diem.Add(diem);
                }

                diem.DiemChuyenCan = diemChuyenCan[i];
                diem.DiemGiuaKy = diemGiuaKy[i];
                diem.DiemThucHanh = diemThucHanh[i];
                diem.DiemCuoiKy = diemCuoiKy[i];
                diem.DiemTrungBinh = TinhDiemTrungBinh(diem);
                diem.XepLoai = TinhXepLoai(diem.DiemTrungBinh);
                diem.NgayCapNhat = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: NhapDiem_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NhapDiem_23TH0003Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhapDiem_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NhapDiem_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NhapDiem_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NhapDiem_23TH0003Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NhapDiem_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
