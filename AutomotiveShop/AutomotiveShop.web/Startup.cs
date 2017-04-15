using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomotiveShop.web.Startup))]
namespace AutomotiveShop.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
