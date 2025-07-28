using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Text;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class SinhVien_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly FileService _file;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserService _userService;
        public SinhVien_23TH0003Controller(MainDbContext context, FileService file, UserManager<IdentityUser> userManager, UserService userService)
        {
            _context = context;
            _file = file;
            _userManager = userManager;
            _userService = userService;
        }

        #region Tài khoản
        public ActionResult TaiKhoan(int IdSinhVien)
        {
            var sinhVien = _context.SinhVien.Find(IdSinhVien);
            if (sinhVien == null)
            {
                return NotFound();
            }
            TaiKhoanSinhVien model = new TaiKhoanSinhVien();
            model.IdSinhVien = IdSinhVien;
            if (!string.IsNullOrEmpty(sinhVien.UserId))
            {
                var user = _userManager.FindByIdAsync(sinhVien.UserId).Result;
                if (user != null)
                {
                    model.Email = user.Email;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TaiKhoanAsync(TaiKhoanSinhVien model)
        {
            var sinhVien = await _context.SinhVien.FindAsync(model.IdSinhVien);
            if (sinhVien == null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(sinhVien.UserId);
            // Nếu chưa có tài khoản, tạo mới
            if (user == null)
            {
                Task<IdentityResult> result = _userService.ThemTaiKhoanAsync(Role.SinhVien.ToString(), model.Email, model.Password);
                if (!result.Result.Succeeded)
                {
                    ModelState.AddModelError("", result.Result.Errors.First().Description);
                    return View(model);
                }
                sinhVien.UserId = _userManager.FindByEmailAsync(model.Email).Result.Id;
                _context.Update(sinhVien);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm tài khoản sinh viên thành công";
                return RedirectToAction("Edit", new { id = model.IdSinhVien });
            }
            // Cập nhật tài khoản gồm email, pass, số điện thoại
            else
            {
                _userService.UpdateTaiKhoan(sinhVien.UserId, model.Email, model.Password);
            }

            return RedirectToAction("TaiKhoan", new { IdSinhVien = model.IdSinhVien });
        }
        #endregion
        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            var model = _context.SinhVien.Include(x => x.Lop).Where(x => x.IsDeleted != true).ToList();
            return View(model);
        }

        // GET: GiangVien_23TH0003/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GiangVien_23TH0003/Create
        public ActionResult Create()
        {
            ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
            var model = new SinhVien();
            return View(model);
        }

        // POST: GiangVien_23TH0003/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(SinhVien model, IFormFile HinhDaiDienFile)
        {
            try
            {
                if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
                {
                    string fileName = await _file.UploadAndGetResultStringAsync(HinhDaiDienFile, model.HinhDaiDien);
                    model.HinhDaiDien = fileName;
                }
                _context.SinhVien.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm sinh viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendLine("Thêm giảng viên không thành công.");
                sb.AppendLine("Thông báo lỗi: " + ex.Message);

                if (ex.InnerException != null)
                {
                    sb.AppendLine("Chi tiết lỗi bên trong: " + ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        sb.AppendLine("Lỗi sâu hơn: " + ex.InnerException.InnerException.Message);
                    }
                }

                sb.AppendLine("Nguồn lỗi: " + ex.Source);
                sb.AppendLine("StackTrace: " + ex.StackTrace);

                TempData["ErrorMessage"] = sb.ToString();
                ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
                return View(model);
            }
        }

        // GET: GiangVien_23TH0003/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.SinhVien.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
            return View(model);
        }

        // POST: GiangVien_23TH0003/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(SinhVien model, IFormFile HinhDaiDienFile)
        {
            try
            {
                if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
                {
                    string fileName = await _file.UploadAndGetResultStringAsync(HinhDaiDienFile, model.HinhDaiDien);
                    model.HinhDaiDien = fileName;
                }
                _userService.UpdateSinhVien(model);
                TempData["SuccessMessage"] = "Cập nhật sinh viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendLine("Thêm giảng viên không thành công.");
                sb.AppendLine("Thông báo lỗi: " + ex.Message);

                if (ex.InnerException != null)
                {
                    sb.AppendLine("Chi tiết lỗi bên trong: " + ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        sb.AppendLine("Lỗi sâu hơn: " + ex.InnerException.InnerException.Message);
                    }
                }

                sb.AppendLine("Nguồn lỗi: " + ex.Source);
                sb.AppendLine("StackTrace: " + ex.StackTrace);

                TempData["ErrorMessage"] = sb.ToString();
                ViewBag.IdLop = new SelectList(_context.Lop.ToList(), "Id", "TenLop");
                return View(model);
            }
        }



        // POST: GiangVien_23TH0003/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.SinhVien.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Sinh viên không tồn tại";
                return RedirectToAction("Index");
            }
            mon.IsDeleted = true;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Xóa sinh viên thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.SinhVien.Include(x => x.Lop).Where(x => x.IsDeleted == true).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var mon = _context.SinhVien.Find(id);
            if (mon != null)
            {
                try
                {
                    mon.IsDeleted = false;
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Khôi phục sinh viên thành công";
                }catch(Exception ex)
                {
                    var errorMessages = new List<string> { "Lỗi khi lưu vào cơ sở dữ liệu:" };
                    var innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        errorMessages.Add($"- {innerEx.Message}");
                        innerEx = innerEx.InnerException;
                    }
                    TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
                }
               
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.SinhVien.Find(id);
            if (mon != null)
            {
                try
                {
                    _context.SinhVien.Remove(mon);
                    _context.SaveChanges();
                    _file.DeleteFile(mon.HinhDaiDien);
                    TempData["SuccessMessage"] = "Xóa vĩnh viễn sinh viên thành công";
                }
                catch (Exception ex)
                {
                    var errorMessages = new List<string> { "Lỗi khi lưu vào cơ sở dữ liệu:" };
                    var innerEx = ex.InnerException;
                    while (innerEx != null)
                    {
                        errorMessages.Add($"- {innerEx.Message}");
                        innerEx = innerEx.InnerException;
                    }
                    TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
                }
            }
            return RedirectToAction("Trash");
        }
    }
}
