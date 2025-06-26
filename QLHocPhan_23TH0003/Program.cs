using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Extensions;
using QLHocPhan_23TH0003.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 💡 Cấu hình Serilog (ghi log ra file logs/log-2025-06-08.txt, theo ngày)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Log từ mức Debug trở lên
    .WriteTo.File(
        path: "logs/log-.txt",        // Ghi log ra thư mục logs/
        rollingInterval: RollingInterval.Day,  // Tự chia file theo ngày
        retainedFileCountLimit: 10,   // Giữ lại tối đa 10 file log cũ (tùy chọn)
        fileSizeLimitBytes: 10_000_000, // Tùy chọn: 10MB/file (auto roll)
        rollOnFileSizeLimit: true     // Chia file nếu vượt quá giới hạn
    )
    .CreateLogger();

// ⚙️ Tích hợp Serilog vào ASP.NET Core
builder.Host.UseSerilog();
// 💡 End Cấu hình Serilog (ghi log ra file logs/log-2025-06-08.txt, theo ngày)


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// add dbcontext để tạo các migration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//end
// add dbcontext để tạo các migration
builder.Services.AddDbContext<QuanLyHocPhanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//end

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    
    options.SignIn.RequireConfirmedAccount = false;
    // bổ sung bỏ ràng buộc mật khẩu
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    // end bổ sung bỏ ràng buộc mật khẩu
})
    .AddEntityFrameworkStores<MainDbContext>()
    .AddDefaultTokenProviders(); // bổ sung khi thêm role
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); // Bổ sung để load view không cần build lại
builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>(); // Đăng ký tập view với ký tập string
builder.Services.AddRazorPages(); // Bổ sugng khi dùng razor page với Identity

builder.Services.AddTransient<IEmailSender, FakeEmailSender>(); // Đăng ký tạm thời fake email sender trong môi trường phát triển
// Lưu key
builder.Services.ConfigureDataProtection(builder.Environment);
var app = builder.Build();

// Configure the HTTP request pipelines
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // bổ sung Middleware phục vụ các file tĩnh (CSS, JS, hình ảnh)
app.UseRouting();

app.UseAuthentication(); // bổ sung tránh lỗi 403
app.UseAuthorization();

app.MapStaticAssets();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { controller = "Home", action = "Index" },
    dataTokens: new { Namespace = "QLHocPhan_23TH0003.Controllers" });
});


app.MapRazorPages();
   //.WithStaticAssets(); Tạm xóa không rõ nguồn gốc

app.Run();
