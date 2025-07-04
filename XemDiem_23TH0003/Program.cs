﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XemDiem_23TH0003.Data;
using XemDiem_23TH0003.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// add dbcontext để tạo các migration
builder.Services.AddDbContext<XemDiemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//end

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MainDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseStaticFiles(); // bổ sung thêm hình anh, css, js, ...
//.WithStaticAssets();
//app.MapStaticAssets(); // bỏ tạm thời
app.UseRouting();

//app.UseAuthorization(); Tạm thời bỏ 



// Router mặc định
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();
// Cấu hình Router area

// Gọi Router
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
    dataTokens: new { Namespace = "XemDiem_23TH0003.Controllers" });
});
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapCustomRoutes();
//});
//app.MapRazorPages()
//   .WithStaticAssets();

app.Run();
