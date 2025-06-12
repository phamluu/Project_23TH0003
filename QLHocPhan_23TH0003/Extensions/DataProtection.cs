using Microsoft.AspNetCore.DataProtection;

namespace QLHocPhan_23TH0003.Extensions
{
    public static class DataProtection
    {
        public static void ConfigureDataProtection(this IServiceCollection services, IWebHostEnvironment env)
        {
            var keysFolder = Path.Combine(env.ContentRootPath, "DataProtectionKeys");

            if (!Directory.Exists(keysFolder))
            {
                Directory.CreateDirectory(keysFolder);
            }

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                .SetApplicationName("QLHocPhan_23TH003");
        }
    }
}
