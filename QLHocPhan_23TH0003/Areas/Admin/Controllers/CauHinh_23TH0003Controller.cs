using Microsoft.AspNetCore.Mvc;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.ViewModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class CauHinh_23TH0003Controller : BaseAdminController
    {
        private readonly MainDbContext _context;
        private readonly FileService _fileService;
        private readonly CauHinhService _cauHinhService;
        private readonly IHttpClientFactory _httpClientFactory;
        public CauHinh_23TH0003Controller(MainDbContext context, FileService fileService, CauHinhService cauHinhService, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _fileService = fileService;
            _cauHinhService = cauHinhService;
            _httpClientFactory = httpClientFactory;
        }
        
        #region Cấu hình

        public IActionResult Index()
        {
            
            var model = _context.CauHinhHeThong.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult GetData(int draw, int start, int length, string searchValue)
        {
            var query = _context.CauHinhHeThong.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.TenCauHinh.Contains(searchValue) || x.MaCauHinh.Contains(searchValue));
            }
            var recordsTotal = query.Count();
            var antiforgery = HttpContext.RequestServices.GetService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>();
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            string token = tokens.RequestToken;

            var data = query.Skip(start).Take(length).ToList()
                .Select(x =>
                {
                    var deleteForm = ButtonHelper.BuildDeleteFormHtml(
                        x.Id,
                        "/Admin/CauHinh_23TH0003/Delete",
                        token
                    );
                    var editForm = ButtonHelper.BuildEditFormHtml(x.Id, "/Admin/CauHinh_23TH0003/Edit");
                    return new CauHinhViewModel
                    {
                        Id = x.Id,
                        MaCauHinh = x.MaCauHinh,
                        TenCauHinh = x.TenCauHinh,
                        MoTa = x.MoTa,
                        Action = editForm + deleteForm
                    };
                }).ToList();

            return Json(new
            {
                draw,
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CauHinhHeThong model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false, message = "Dữ liệu không hợp lệ" });
            }
            try
            {
                _context.CauHinhHeThong.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Thêm cấu hình thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra";
                return Json(new { status = false, message = TempData["ErrorMessage"] });
            }
        }

        // GET: HocKy_23TH0003Controller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = _context.CauHinhHeThong.Find(id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        // POST: HocKy_23TH0003Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CauHinhHeThong model)
        {
            try
            {
                var data = _context.CauHinhHeThong.Find(model.Id);
                if (data == null)
                {
                    TempData["ErrorMessage"] = "Cấu hình không tồn tại";
                    return View(model);
                }
                data.MaCauHinh = model.MaCauHinh;
                data.TenCauHinh = model.TenCauHinh;
                data.MoTa = model.MoTa;
                _context.Update(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật cấu hình thành công";
                return Json(new { status = true, message = TempData["SuccessMessage"] });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi" + ex.Message);
            }
            TempData["ErrorMessage"] = "Cập nhật cấu hình không thành công";
            return Json(new { status = false, message = TempData["ErrorMessage"] });
        }

        // GET: HocKy_23TH0003Controller/Delete/5


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var cauHinh = _context.CauHinhHeThong.Find(id);
                if (cauHinh == null)
                {
                    TempData["ErrorMessage"] = "Cấu hình không tồn tại";
                    RedirectToAction("Index");
                }

                _context.CauHinhHeThong.Remove(cauHinh);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Xóa cấu hình thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Lỗi khi xóa: " + ex.Message });
            }

        }

        #endregion

        #region Cài đặt hệ thống

        public ActionResult CaiDat()
        {
            var ch = _cauHinhService.GetTatCa();
            return View(ch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CaiDatAsync(List<CauHinhViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                // Thu thập tất cả lỗi từ ModelState
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(msg => !string.IsNullOrWhiteSpace(msg))
                    .ToList();

                // Gộp lại thành 1 chuỗi
                string fullErrorMessage = string.Join(" | ", errorMessages);

                // Đưa vào TempData để hiển thị ở View (hoặc log)
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ: " + fullErrorMessage;

                return View(model);
            }

            for (int i = 0; i < model.Count; i++)
            {
                var item = model[i];

                // ✅ Xử lý upload ảnh nếu có
                if (item.LoaiCauHinh == LoaiCauHinh.Image && item.HinhDaiDienFile != null)
                {
                    _fileService.DeleteFile(item.GiaTri);
                    string fileName = await _fileService.UploadAndGetResultStringAsync(item.HinhDaiDienFile);
                    item.GiaTri = fileName;
                }

                // ✅ Cập nhật vào DB
                var entity = _context.CauHinhHeThong.FirstOrDefault(x => x.MaCauHinh == item.MaCauHinh);
                if (entity != null)
                {
                    entity.GiaTri = item.GiaTri;
                    _context.CauHinhHeThong.Update(entity);
                }
                else
                {
                    _context.CauHinhHeThong.Add(new CauHinhHeThong
                    {
                        MaCauHinh = item.MaCauHinh,
                        GiaTri = item.GiaTri
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật cấu hình thành công!";
                return RedirectToAction("CaiDat");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - Chi tiết: {ex.InnerException.Message}"
                    : ex.Message;
                ModelState.AddModelError(string.Empty, errorMessage);
                TempData["ErrorMessage"] = "Lỗi khi lưu dữ liệu: " + errorMessage;

                return View(model);
            }
        }


        #endregion

        //[HttpGet("callback")]
        //public async Task<IActionResult> ChangeCodeRefeshToken(string code)
        //{
        //    var clientId = CauHinhHelper.Get("dropbox_app_key");       // Hoặc hard-code
        //    var clientSecret = CauHinhHelper.Get("dropbox_app_secret");
        //    var redirectUri = "https://localhost:7067/";

        //    var client = _httpClientFactory.CreateClient();
        //    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.dropboxapi.com/oauth2/token");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
        //    request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //        {
        //            { "code", code },
        //            { "grant_type", "authorization_code" },
        //            { "redirect_uri", redirectUri }
        //        });

        //    var response = await client.SendAsync(request);
        //    var content = await response.Content.ReadAsStringAsync();

        //    if (!response.IsSuccessStatusCode)
        //        return Content("Lỗi: " + content, "text/plain");

        //    using var doc = JsonDocument.Parse(content);
        //    var accessToken = doc.RootElement.GetProperty("access_token").GetString();
        //    var refreshToken = doc.RootElement.GetProperty("refresh_token").GetString();

        //    _cauHinhService.UpdateGiaTri("dropbox_token", accessToken!);
        //    _cauHinhService.UpdateGiaTri("dropbox_refresh_token", refreshToken!);

        //    return Content("✅ Lấy token thành công:\n" + content, "text/plain");
        //}
    }
}
