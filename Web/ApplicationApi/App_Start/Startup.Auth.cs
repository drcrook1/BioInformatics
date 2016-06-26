using BioInfo.Web.ApplicationApi.Models.Security;
using BioInfo.Web.ApplicationApi.Models.Security.Helpers;
using BioInfo.Web.ApplicationApi.Models.Security.Managers;
using BioInfo.Web.ApplicationApi.Models.Security.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleOAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            ConfigureToUseSingleInstancePerRequest(app);
            //EnableApplicationToUseCookies(app);
            ConfigureApplicationForOAuthFlow();
            EnableApplicationToUseBearerTokens(app);


            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseCors(CorsOptions.AllowAll);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            GoogleOAuthOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = AppSettingsHelper.LoginProviders.Google.ClientId,
                ClientSecret = AppSettingsHelper.LoginProviders.Google.ClientSecret,
                Provider = new GoogleOAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleOAuthOptions);
        }

        private static void EnableApplicationToUseBearerTokens(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
            //app.UseOAuthAuthorizationServer(OAuthOptions);
        }

        private static void ConfigureApplicationForOAuthFlow()
        {
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        private static void EnableApplicationToUseCookies(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        private static void ConfigureToUseSingleInstancePerRequest(IAppBuilder app)
        {
            app.CreatePerOwinContext(SecurityDBContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<RefreshTokenManager>(RefreshTokenManager.Create);
            app.CreatePerOwinContext<ClientManager>(ClientManager.Create);

        }
    }
}
