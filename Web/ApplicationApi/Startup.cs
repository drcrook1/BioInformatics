using BioInfo.Web.ApplicationApi.Models.Security.Managers;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(BioInfo.Web.ApplicationApi.Startup))]
namespace BioInfo.Web.ApplicationApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);
        }
    }
}