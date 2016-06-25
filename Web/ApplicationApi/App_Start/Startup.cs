using BioInfo.Web.ApplicationApi.Models.IdentityServer;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using static IdentityServer3.Core.Constants;

[assembly: OwinStartup(typeof(BioInfo.Web.ApplicationApi.App_Start.Startup))]

namespace BioInfo.Web.ApplicationApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // token generation
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    //SigningCertificate = LoadCertificate(),

                    Factory = new IdentityServerServiceFactory()
                                .UseInMemoryUsers(Users.Get())
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(StandardScopes.All)
                });
            });
        
            UnityConfig.RegisterComponents();
            // web api configuration
            app.UseWebApi(WebApiConfig.Register());
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
