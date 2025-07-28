using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Text;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class GiangVien_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly FileService _file;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;

        public GiangVien_23TH0003Controller(MainDbContext context, FileService file, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            UserService userService
            )
        {
            _context = context;
            _file = file;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        #region Hồ sơ giảng viên
        // GET: GiangVien_23TH0003
        public ActionResult Index()
        {
            var model = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsDeleted != true).OrderByDescending(x => x.NgayTao).ToList();
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
            ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
            var model = new GiangVien();
            return View(model);
        }

        // POST: GiangVien_23TH0003/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(GiangVien model, IFormFile HinhDaiDienFile)
        {
            try
            {
                if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
                {
                    string fileName = await _file.UploadAndGetResultStringAsync(HinhDaiDienFile, model.HinhDaiDien);
                    model.HinhDaiDien = fileName;
                }
                _context.GiangVien.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm giảng viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
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
                ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
                return View(model);
            }
        }

        // GET: GiangVien_23TH0003/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.GiangVien.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
            return View(model);
        }

        // POST: GiangVien_23TH0003/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(GiangVien model, IFormFile HinhDaiDienFile)
        {
            try
            {
                if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
                {
                    string fileName = await _file.UploadAndGetResultStringAsync(HinhDaiDienFile, model.HinhDaiDien);
                    model.HinhDaiDien = fileName;
                }
                _userService.UpdateGiangVien(model);
                TempData["SuccessMessage"] = "Cập nhật giảng viên thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.IdKhoa = new SelectList(_context.Khoa.ToList(), "Id", "TenKhoa");
                return View(model);
            }
        }



        // POST: GiangVien_23TH0003/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon == null)
            {
                TempData["ErrorMessage"] = "Giảng viên không tồn tại";
                return RedirectToAction("Index");
            }
            try
            {
                mon.IsDeleted = true;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa giảng viên thành công";
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
            
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var deletedLop = _context.GiangVien.Include(x => x.Khoa).Where(x => x.IsDeleted == true).OrderByDescending(x => x.NgayTao).ToList();
            return View(deletedLop);
        }

        // Khôi phục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon != null)
            {
                mon.IsDeleted = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Khôi phục giảng viên thành công";
            }
            return RedirectToAction("Trash");
        }

        // Xóa vĩnh viễn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(int id)
        {
            var mon = _context.GiangVien.Find(id);
            if (mon != null)
            {
                _context.GiangVien.Remove(mon);
                _context.SaveChanges();
                _file.DeleteFile(mon.HinhDaiDien);
                TempData["SuccessMessage"] = "Xóa vĩnh viễn giảng viên thành công";
            }
            return RedirectToAction("Trash");
        }

        #endregion

        #region Duyệt Giảng viên
        public ActionResult Duyet(int id, bool IsActive)
        {
            var giangVien = _context.GiangVien.Find(id);
            if (giangVien != null)
            {
                giangVien.IsActive = IsActive;
                _context.SaveChanges();
                if (IsActive)
                {
                    TempData["SuccessMessage"] = "Duyệt giảng viên thành công";
                }
                else
                {
                    TempData["SuccessMessage"] = "Hủy Duyệt giảng viên thành công";
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Tài khoản của giảng viên
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        public ActionResult TaiKhoan(int IdGiangVien)
        {
            var giangVien = _context.GiangVien.Find(IdGiangVien);
            if (giangVien == null)
            {
                return NotFound();
            }
            TaiKhoanGiangVien model = new TaiKhoanGiangVien();
            model.IdGiangVien = IdGiangVien;
            if (!string.IsNullOrEmpty(giangVien.UserId))
            {
               var user = _userManager.FindByIdAsync(giangVien.UserId).Result;
                if (user != null)
                {
                    model.Email = user.Email;
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TaiKhoanAsync(TaiKhoanGiangVien model)
        {
            var giangVien = await _context.GiangVien.FindAsync(model.IdGiangVien);
            if (giangVien == null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(giangVien.UserId);
            // Nếu chưa có tài khoản, tạo mới
            if (user == null)
            {
                Task<IdentityResult> result = _userService.ThemTaiKhoanAsync(Role.GiangVien.ToString(), model.Email, model.Password);
                if (!result.Result.Succeeded)
                {
                    ModelState.AddModelError("", result.Result.Errors.First().Description);
                    return View(model);
                }
                giangVien.UserId = _userManager.FindByEmailAsync(model.Email).Result.Id;
                _context.Update(giangVien);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm tài khoản thành công";
                return RedirectToAction("Edit", new { id = model.IdGiangVien });
            }
            // Cập nhật tài khoản gồm email, pass, số điện thoại
            else
            {
                _userService.UpdateTaiKhoan(giangVien.UserId, model.Email, model.Password);
            }

            return RedirectToAction("TaiKhoan", new { IdGiangVien = model.IdGiangVien });
        }

        #endregion
    }
}
