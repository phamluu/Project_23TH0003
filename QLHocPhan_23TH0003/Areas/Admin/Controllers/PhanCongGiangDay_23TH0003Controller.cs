using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class PhanCongGiangDay_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        public PhanCongGiangDay_23TH0003Controller(MainDbContext context, IRazorViewToStringRenderer renderer, LinkGenerator linkGenerator)
        {
            _context = context;
        }
        public ActionResult Index(int? IdLopHocPhan)
        {
            
            var lhp = _context.LopHocPhan.Include(x => x.HocPhan).ThenInclude(x => x.HocKy)
                .Include(x => x.PhanCongGiangDays).ThenInclude(x => x.GiangVien).Where(x => x.IsDeleted != true).ToList();
            var giangVien = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsActive == true && x.IsDeleted != true).ToList();
            var daPhanCong = _context.PhanCongGiangDay
                             .Where(p => p.IdLopHocPhan == IdLopHocPhan)
                             .Select(p => p.IdGiangVien)
                             .ToList();
            PhanCongGiangDayViewModel model = new PhanCongGiangDayViewModel();
            model.LopHocPhans = lhp;
            model.GiangViens = giangVien;
            model.IdLopHocPhan = IdLopHocPhan;
            model.GiangVienDaPhanCong = daPhanCong;
            if (!IdLopHocPhan.HasValue)
            {
                model.IdLopHocPhan = lhp.LastOrDefault().Id;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhanCong(int IdLopHocPhan, int[] IdGiangVien)
        {
            // Lấy các phân công hiện tại
            var phanCongHienTai = _context.PhanCongGiangDay
                .Where(p => p.IdLopHocPhan == IdLopHocPhan)
                .ToList();

            // Nếu người dùng không chọn gì thì xem như danh sách trống
            var idGuiTuForm = IdGiangVien?.ToList() ?? new List<int>();

            // Danh sách các IdGiangVien đã có trong DB
            var idTrongDb = phanCongHienTai.Select(p => p.IdGiangVien).ToList();

            // 1. Thêm mới nếu có trong form nhưng không có trong DB
            var canThem = idGuiTuForm.Except(idTrongDb).ToList();
            foreach (var idGV in canThem)
            {
                var newPhanCong = new PhanCongGiangDay
                {
                    IdLopHocPhan = IdLopHocPhan,
                    IdGiangVien = idGV
                };
                _context.PhanCongGiangDay.Add(newPhanCong);
            }

            // 2. Xoá nếu có trong DB nhưng không có trong form
            var canXoa = phanCongHienTai
                .Where(p => !idGuiTuForm.Contains(p.IdGiangVien))
                .ToList();
            _context.PhanCongGiangDay.RemoveRange(canXoa);

            
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Phân công giảng viên thành công";
            return RedirectToAction("Index", new { IdLopHocPhan });
        }


    }
}
