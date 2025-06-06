namespace XemDiem_23TH0003.Extensions
{
    public static class EndpointRouteExtensions
    {
        public static void MapCustomRoutes(this IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapAreaControllerRoute(
            //    name: "SinhVienArea",
            //    areaName: "SinhVien",
            //    pattern: "SinhVien/{controller=Home}/{action=Index}/{id?}");

            //endpoints.MapAreaControllerRoute(
            //    name: "GiangVienArea",
            //    areaName: "GiangVien",
            //    pattern: "GiangVien/{controller=Home}/{action=Index}/{id?}");
            //endpoints.MapAreaControllerRoute(
            //    name: "AdminArea",
            //    areaName: "Admin",
            //    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}",
                defaults: new { controller = "Home", action = "Index" },
                 dataTokens: new { Namespace = "XemDiem_23TH0003.Controllers" }
            );
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}",
                //    constraints: new { area = "" } 
                //)
                //.WithStaticAssets();


            endpoints.MapRazorPages()
               .WithStaticAssets();
        }
    }
}
