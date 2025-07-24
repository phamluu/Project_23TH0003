

using NuGet.Packaging;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] ExportToExcel<T>(List<T> data, string sheetName, string reportTitle, string[] headers, string userName, Func<T, object[]> mapRow)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add(sheetName);

            // Gộp STT + headers
            var allHeaders = new[] { "STT" }.Concat(headers).ToArray();

            // Tiêu đề lớn
            ws.Cells["A1"].Value = reportTitle;
            ws.Cells[1, 1, 1, allHeaders.Length].Merge = true;
            ws.Cells["A1"].Style.Font.Size = 16;
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells["A2"].Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy}";
            ws.Cells["A2:C2"].Merge = true;
            ws.Cells["A3"].Value = $"Người xuất: {userName}";
            ws.Cells["A3:C3"].Merge = true;
            // Header
            for (int i = 0; i < allHeaders.Length; i++)
            {
                ws.Cells[5, i + 1].Value = allHeaders[i];
                ws.Cells[5, i + 1].Style.Font.Bold = true;
                ws.Cells[5, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[5, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[5, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            // Ghi dữ liệu
            int row = 6;
            int stt = 1;
            foreach (var item in data)
            {
                var values = mapRow(item);
                ws.Cells[row, 1].Value = stt++;
                for (int i = 0; i < values.Length; i++)
                {
                    ws.Cells[row, i + 2].Value = values[i];
                    ws.Cells[row, i + 2].Style.Border.BorderAround(ExcelBorderStyle.Hair);
                }
                row++;
            }

            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            return package.GetAsByteArray();
        }

    }
}
