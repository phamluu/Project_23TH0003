using QLHocPhan_23TH0003.Enums;
using QLHocPhan_23TH0003.ViewModel;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class CauHinhHelper
    {
        private static IHttpContextAccessor? _httpContextAccessor;
        public static void Configure(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }
        public static List<CauHinhViewModel> GetCauHinhCoBan()
        {
            return new List<CauHinhViewModel>
            {
                new CauHinhViewModel { MaCauHinh = "smtp_user", TenCauHinh = "Email gửi SMTP", LoaiCauHinh = LoaiCauHinh.Text },
                new CauHinhViewModel { MaCauHinh = "smtp_pass", TenCauHinh = "Mật khẩu Email SMTP", LoaiCauHinh = LoaiCauHinh.Text },

                // Dropbox
                new CauHinhViewModel { MaCauHinh = "dropbox_token", TenCauHinh = "Dropbox token", LoaiCauHinh = LoaiCauHinh.TextArea },
                 // Dropbox

                new CauHinhViewModel { MaCauHinh = "HocPhi", TenCauHinh = "Học phí 1 tín chỉ", LoaiCauHinh = LoaiCauHinh.Number },
                new CauHinhViewModel { MaCauHinh = "Banner", TenCauHinh = "Banner", LoaiCauHinh = LoaiCauHinh.Image },
                new CauHinhViewModel { MaCauHinh = "Footer", TenCauHinh = "Nội dung chân web", LoaiCauHinh = LoaiCauHinh.Editor }
            };
        }

        public static string? Get(string key)
        {
            var context = _httpContextAccessor?.HttpContext;
            var dict = context?.Items["CauHinhLayout"] as Dictionary<string, string>;
            return dict != null && dict.ContainsKey(key) ? dict[key] : null;
        }
    }
}
