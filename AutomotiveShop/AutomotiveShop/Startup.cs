using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomotiveShop.Startup))]
namespace AutomotiveShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
