using BioInfo.Web.ApplicationApi.Models.Security.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = (context.OwinContext.Get<string>("as:clientAllowedOrigin")) ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName, context.ClientId);
            //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            var ticket = CreateAuthenticationTicket(user, OAuthDefaults.AuthenticationType, context.ClientId);
            context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "Client ID is required.");
                return Task.FromResult<object>(null);
            }
            var clientManager = context.OwinContext.Get<ClientManager>();
            var client = clientManager.FindClient(clientId);
            if (client == null)
            {
                context.SetError("invalid_clientId", "Client is not registered.");
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client Secret was not provided.");
                    return Task.FromResult<object>(null);
                }
                var passwordHasher = new PasswordHasher();
                if (client.Secret != passwordHasher.HashPassword(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client Secret is invalid.");
                    return Task.FromResult<object>(null);
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/oauth2callback");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token not valid for the current client");
                return;
            }

            // Refresh Identity
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var userName = context.Ticket.Properties.Dictionary["userName"];
            ApplicationUser user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName, currentClient);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
        }

        public override async Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            var allowedOrigin = (context.OwinContext.Get<string>("as:clientAllowedOrigin")) ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var parameters = context.Parameters;
            var grantType = parameters["grant_type"];
            if (grantType == "confirm_email")
            {
                var userid = parameters["userid"];
                var code = parameters["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    code = code.Replace(" ", "+");
                }

                IdentityResult result;
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                try
                {
                    result = await userManager.ConfirmEmailAsync(userid, code);
                }
                catch (InvalidOperationException ex)
                {
                    context.SetError("Invalid User ID");
                    return;
                }

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        context.SetError(error);
                    }
                    //context.Rejected();
                    return;
                }

                ApplicationUser user = await userManager.FindByIdAsync(userid);
                if (user != null)
                {
                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                    AuthenticationProperties properties = CreateProperties(user.UserName, context.ClientId);
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                    context.Validated(ticket);
                    return;
                }


            }

            context.Rejected();

        }

        public static AuthenticationProperties CreateProperties(string userName, string clientId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                 {"as:client_id", clientId ?? string.Empty},
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        internal AuthenticationTicket CreateAuthenticationTicket(ApplicationUser user, string authenticationType, string clientId)
        {
            var identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            foreach (var identityUserClaim in user.Claims)
            {
                identity.AddClaim(new Claim(identityUserClaim.ClaimType, identityUserClaim.ClaimValue));
            }

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"as:client_id", clientId ?? string.Empty},
                {"userName", user.UserName},
                {"profileId", user.Id}

            };

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                data.Add("firstName", user.FirstName);
            }

            if (!string.IsNullOrEmpty(user.LastName))
            {
                data.Add("lastName", user.LastName);
            }

            foreach (var identityUserClaim in user.Claims)
            {
                if (identityUserClaim.ClaimType == ClaimTypes.Role)
                {
                    if (data.ContainsKey("roles"))
                    {
                        data["roles"] += string.Format(",{0}", identityUserClaim.ClaimValue);
                    }
                    else
                    {
                        data.Add("roles", identityUserClaim.ClaimValue);
                    }
                }

                if (identityUserClaim.ClaimType == ApplicationClaims.Realm)
                {
                    data[ApplicationClaims.Realm] = identityUserClaim.ClaimValue;
                }
            }

            AuthenticationProperties properties = new AuthenticationProperties(data);
            return new AuthenticationTicket(identity, properties);
        }
    }
}