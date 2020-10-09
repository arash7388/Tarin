using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inv.Startup))]
namespace Inv
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
