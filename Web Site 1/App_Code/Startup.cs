using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Site_1.Startup))]
namespace Web_Site_1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
