using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLHocPhan_23TH0003.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MainDbContext>();
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); // Bổ sung để load view không cần build lại

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
