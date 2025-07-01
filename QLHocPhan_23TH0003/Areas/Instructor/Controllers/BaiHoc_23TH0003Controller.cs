using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.IO;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Instructor.Controllers
{
    public class BaiHoc_23TH0003Controller : BaseInstructorController
    {
        private readonly MainDbContext _context;
        private readonly DropboxService _dropbox;
        public BaiHoc_23TH0003Controller(MainDbContext context, DropboxService dropbox)
        {
            _context = context;
            _dropbox = dropbox;
        }
        public ActionResult Index(int IdLopHocPhan)
        {
            // Chỉ giảng viên đang giảng dạy lớp học phần này mới xem được
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int IdGiangVien = _context.GiangVien.FirstOrDefault(x => x.UserId == UserId).Id;
            var lhp = _context.LopHocPhan.Include(p => p.HocPhan)
                .Include(x => x.PhanCongGiangDays).ThenInclude(x => x.GiangVien)
                .Where(p => p.Id == IdLopHocPhan && p.PhanCongGiangDays
                .Any(p => p.IdGiangVien == IdGiangVien)).FirstOrDefault();

            var baihoc = _context.BaiHoc
                .Where(x => x.IdLopHocPhan == IdLopHocPhan && x.LopHocPhan.PhanCongGiangDays.Any(p => p.IdGiangVien == IdGiangVien))
                .Select(x => new BaiHocViewModel
                {
                    Id = x.Id,
                    IdLopHocPhan = x.IdLopHocPhan,
                    TenBaiHoc = x.TenBaiHoc,
                    NoiDung = x.NoiDung,
                    Video = x.Video,
                    TaiLieu = x.TaiLieu,
                    DropboxFile = DropboxParserHelper.Parse(x.TaiLieu),
                    DropboxVideo = DropboxParserHelper.Parse(x.Video)
                });
            if (lhp == null) return NotFound();
            ViewBag.LopHocPhan = lhp;
            return View(baihoc);
        }

        // GET: BaiHoc_23TH0003Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BaiHoc_23TH0003Controller/Create
        public ActionResult Create(int IdLopHocPhan)
        {
            BaiHoc model = new BaiHoc() { IdLopHocPhan = IdLopHocPhan };
            return View(model);
        }

        // POST: BaiHoc_23TH0003Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(BaiHoc model, IFormFile? VideoFile, IFormFile? TaiLieuFile)
        {
            try
            {
                if (VideoFile != null && VideoFile.Length > 0)
                {
                    model.Video = await _dropbox.UploadAndGetResultStringAsync(VideoFile);
                }

                if (TaiLieuFile != null && TaiLieuFile.Length > 0)
                {
                    model.TaiLieu = await _dropbox.UploadAndGetResultStringAsync(TaiLieuFile);
                }
                _context.BaiHoc.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm bài học thành công";
                return RedirectToAction(nameof(Index), new { IdLopHocPhan = model.IdLopHocPhan });
            }
            catch
            {
                TempData["ErrorMessage"] = "Thêm bài học không thành công";
                return View(model);
            }
        }

        // GET: BaiHoc_23TH0003Controller/Edit/5
        public ActionResult Edit(int id)
        {
            var baiHoc = _context.BaiHoc.Find(id);
            if (baiHoc == null)
            {
                return NotFound();
            }

            // Giả sử bạn đã có cách lấy thông tin Dropbox từ DB hoặc API
            var dropboxFile = DropboxParserHelper.Parse(baiHoc.TaiLieu);
            var dropboxVideo = DropboxParserHelper.Parse(baiHoc.Video);
            ViewBag.DropboxFile = dropboxFile;
            ViewBag.DropboxVideo = dropboxVideo;
            return View(baiHoc);
        }


        // POST: BaiHoc_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(BaiHoc model, IFormFile? VideoFile, IFormFile? TaiLieuFile)
        {
            try
            {
                var baiHocInDb = await _context.BaiHoc.FindAsync(model.Id);
                if (baiHocInDb == null)
                {
                    TempData["ErrorMessage"] = "Bài học không tồn tại.";
                    return RedirectToAction(nameof(Index));
                }

                baiHocInDb.TenBaiHoc = model.TenBaiHoc;
                baiHocInDb.NoiDung = model.NoiDung;
                baiHocInDb.IdLopHocPhan = model.IdLopHocPhan;

                if (VideoFile != null && VideoFile.Length > 0)
                {
                    baiHocInDb.Video = await _dropbox.UploadAndGetResultStringAsync(VideoFile);
                }

                if (TaiLieuFile != null && TaiLieuFile.Length > 0)
                {
                    baiHocInDb.TaiLieu = await _dropbox.UploadAndGetResultStringAsync(TaiLieuFile);
                }
                _context.BaiHoc.Update(baiHocInDb);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật bài học thành công";
                return RedirectToAction(nameof(Index), new { IdLopHocPhan = model.IdLopHocPhan });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Cập nhật bài học không thành công: " + ex.Message;
                return View(model);
            }
        }


        // POST: BaiHoc_23TH0003Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = _context.BaiHoc.Find(id);
                _context.BaiHoc.Remove(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa bài học thành công";
                return RedirectToAction(nameof(Index), new { IdLopHocPhan = model.IdLopHocPhan });
            }
            catch
            {
                TempData["ErrorMessage"] = "Xóa bài học không thành công";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
