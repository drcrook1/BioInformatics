using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BioInfo.Web.AdminWeb.Startup))]
namespace BioInfo.Web.AdminWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
