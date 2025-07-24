using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLHocPhan_23TH0003.Common.Helpers;
using QLHocPhan_23TH0003.Service;
using System.Drawing;
using System.Security.Claims;

namespace QLHocPhan_23TH0003.Areas.Admin.Controllers
{
    public class ThongKe_23TH0003Controller : BaseAdminController
    {
        private readonly BaoCaoService _service;
        public ThongKe_23TH0003Controller(BaoCaoService service)
        {
            _service = service;
        }
        #region Báo cáo sinh viên
        public IActionResult BaoCaoSinhVien()
        {
            var model = _service.GetBaoCaoSinhVien();
            return View(model);
        }

        public IActionResult XuatBaoCaoSinhVienExcel()
        {
            var data = _service.GetBaoCaoSinhVien();

            var headers = new[] { "Mã lớp", "Tên lớp", "Khoa", "Số SV nam", "Số SV nữ", "Tổng số SV" };

            string user = User.Identity.Name;
            byte[] fileBytes = ExcelHelper.ExportToExcel(
                data.ToList(),
                "SinhVien",
                "BÁO CÁO THỐNG KÊ SINH VIÊN",
                headers,
                user,
                item => new object[] {
            item.MaLop,
            item.TenLop,
            item.Khoa,
            item.SLNam,
            item.SLNu,
            item.TongSV
                }
            );

            var fileName = $"BaoCaoSinhVien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        #endregion

        #region Báo cáo học phần
        public IActionResult BaoCaoHocPhan()
        {
            var model = _service.GetBaoCaoHocPhan();
            return View(model);
        }
        #endregion

        #region Báo cáo kết quả học tập
        public IActionResult BaoCaoDiem()
        {
            return View();
        }
        #endregion

        #region Báo cáo học phí

        public IActionResult BaoCaoHocPhi()
        {
            return View();
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
