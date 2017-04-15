using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomotiveShop.web.web.web.Startup))]
namespace AutomotiveShop.web.web.web.web.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
