using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EngineOverflow.Web.Startup))]
namespace EngineOverflow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
