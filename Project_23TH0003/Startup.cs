using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_23TH0003.Startup))]
namespace Project_23TH0003
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
