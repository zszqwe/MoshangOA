using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moshang.OA.UI.Portal.Startup))]
namespace Moshang.OA.UI.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
