using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLHocPhan_23TH0003.Data;
using QLHocPhan_23TH0003.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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


app.MapRazorPages()
   .WithStaticAssets();

app.Run();
