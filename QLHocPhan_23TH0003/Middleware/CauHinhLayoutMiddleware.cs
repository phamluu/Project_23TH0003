using QLHocPhan_23TH0003.Service;

namespace QLHocPhan_23TH0003.Middleware
{
    public class CauHinhLayoutMiddleware
    {
        private readonly RequestDelegate _next;

        public CauHinhLayoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CauHinhService cauHinhService)
        {
            // Lấy toàn bộ cấu hình từ DB
            var cauHinh = cauHinhService.GetTatCa(); // Dictionary<string, string?>
            var dict = cauHinh.ToDictionary(x => x.MaCauHinh, x => x.GiaTri ?? "");

            context.Items["CauHinhLayout"] = dict;

            await _next(context);
        }
    }
}
