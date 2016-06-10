using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BioInfo.Web.ExternalWeb.Startup))]
namespace BioInfo.Web.ExternalWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
