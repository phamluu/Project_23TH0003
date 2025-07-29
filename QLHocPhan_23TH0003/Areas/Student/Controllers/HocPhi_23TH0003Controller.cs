using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.Models;
using QLHocPhan_23TH0003.Service;
using QLHocPhan_23TH0003.Service.Api;
using QLHocPhan_23TH0003.ViewModel;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Student.Controllers
{
    public class HocPhi_23TH0003Controller : BaseStudentController
    {
        private readonly QuanLyHocPhanDbContext _context;
        private readonly ApplicationDbContext _applicationDb;
        private readonly HocPhiService _service;
        private readonly VietQRPaymentService _vietQRPaymentService;
        private readonly ZaloPayService _zaloPayService;
        private readonly VNPayService _vNPayService;
        public HocPhi_23TH0003Controller(QuanLyHocPhanDbContext context, 
            ApplicationDbContext applicationDb, 
            HocPhiService service,
            VietQRPaymentService vietQRPaymentService,
            ZaloPayService zaloPayService,
            VNPayService vNPayService
            )
        {
            _context = context;
            _applicationDb = applicationDb;
            _service = service;
            _vietQRPaymentService = vietQRPaymentService;
            _zaloPayService = zaloPayService;
            _vNPayService = vNPayService;
        }
        // Xem thông tin học phí
        public ActionResult Index()
        {
            try
            {
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == currentUserId);

                if (sinhVien == null)
                {
                    TempData["ErrorMessages"] = new List<string> { "Sinh viên chưa có hồ sơ." };
                    return View();
                }

                HocPhiTongHopViewModel model = new HocPhiTongHopViewModel
                {
                    HocKy = _context.HocKy.ToList(),
                    HocPhiHocKy = _service.GetHocPhiHocKy(sinhVien.Id)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { $"Lỗi chính: {ex.Message}" };
                var inner = ex.InnerException;
                while (inner != null)
                {
                    errors.Add($"Lỗi lồng: {inner.Message}");
                    inner = inner.InnerException;
                }

                TempData["ErrorMessages"] = errors;
                return View();
            }
        }

        #region Thanh toán học phí
        public ActionResult Step1(int IdHocKy)
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == CurrentUserId);
            if (sinhVien == null)
            {
                TempData["ErrorMessage"] = "Sinh viên chưa có hồ sơ";
                return View();
            }
            HocPhiHocKyViewModel model = _service.GetHocPhi(sinhVien.Id, IdHocKy);
            return View(model);
        }

        // GET: HocPhi_23TH0003Controller/Details/5
        public async Task<ActionResult> ThanhToanAsync(int Method, int IdHocKy)
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sinhVien = _context.SinhVien.FirstOrDefault(x => x.UserId == CurrentUserId);
            if (sinhVien == null)
            {
                TempData["ErrorMessage"] = "Sinh viên chưa có hồ sơ";
                return View();
            }
            HocPhiHocKyViewModel model = _service.GetHocPhi(sinhVien.Id, IdHocKy);
            string Addinfo = "";
            // Lưu trước hóa đơn
            _context.ThanhToanHocPhi.Add(new ThanhToanHocPhi()
            {
                IdHocKy = IdHocKy,
                IdSinhVien = sinhVien.Id,
                NgayThanhToan = DateTime.Now,
                PhuongThuc = Method,
                SoTien = (int)model.ConNo
            });
            _context.SaveChanges();
            // Lấy lại Số hóa đơn vừa tạo
            var hoadon = _context.ThanhToanHocPhi.Where(x => x.IdSinhVien == sinhVien.Id).OrderByDescending(x => x.NgayThanhToan).FirstOrDefault();


            // Thanh toán với việt qr
            if (Method == (int)PhuongThucThanhToan.vietqr)
            {
                Addinfo = "Đóng học phí: " + hoadon.Id;
                ViewBag.VietQrCode = _vietQRPaymentService.PaymentQRCode(
                    (int)model.ConNo,
                    Addinfo
                );
            }
            if (Method == (int)PhuongThucThanhToan.zalopay)
            {
                var paymentUrl = await _zaloPayService.CreateOrderAsync(hoadon.SoTien, hoadon.Id.ToString());
                return Redirect(paymentUrl);
            }

            // Thanh toán với zalo pay
            if (Method == (int)PhuongThucThanhToan.vnpay)
            {
                string paymentUrl = _vNPayService.CreatePaymentUrl(HttpContext, hoadon.SoTien, hoadon.Id.ToString());


                return Redirect(paymentUrl);
            }
            return View(model);
        }

        #region Thanh toán zalo pay
        [HttpPost("callback")]
        public IActionResult Callback() => Ok();

        [HttpGet("return")]
        public IActionResult Return() => View(); // trang thank you hoặc kết quả
        #endregion

        #region Thanh toán vnpay
        public IActionResult PaymentReturn()
        {
            var isValid = _vNPayService.ValidateResponse(Request.Query);

            if (isValid)
            {
                // ✅ Thanh toán hợp lệ
                ViewBag.Message = "Thanh toán thành công!";
            }
            else
            {
                // ❌ Sai chữ ký hoặc dữ liệu giả mạo
                ViewBag.Message = "Thanh toán thất bại! Sai chữ ký.";
            }

            return View(); // Tạo View PaymentReturn.cshtml để hiển thị
        }
        #endregion
        #endregion

    }
}
