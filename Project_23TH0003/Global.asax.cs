using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Project_23TH0003.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Claims;
using System.Web.Helpers;

namespace Project_23TH0003
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            //// Cấu hình Identity
            //var context = new ApplicationDbContext();
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //// Tạo role nếu chưa có (ví dụ: "Admin")
            //var role = new IdentityRole("Admin");
            //if (!roleManager.RoleExists("Admin"))
            //{
            //    roleManager.Create(role);
            //}

            //// Tiếp tục với các cấu hình khác của ứng dụng
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
