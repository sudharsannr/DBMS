using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GourmetGuide.Startup))]
namespace GourmetGuide
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
