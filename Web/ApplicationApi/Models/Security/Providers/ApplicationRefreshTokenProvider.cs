using BioInfo.Web.ApplicationApi.Models.Security.Managers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Providers
{
    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary["as:client_id"];
            var refreshTokenManager = context.OwinContext.Get<RefreshTokenManager>();
            if (string.IsNullOrEmpty(clientId))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshToken
            {
                Id = refreshTokenId,
                ClientId = clientId,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await refreshTokenManager.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var refreshTokenManager = context.OwinContext.Get<RefreshTokenManager>();
            var allowOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowOrigin });

            var refreshToken = await refreshTokenManager.FindRefreshToken(context.Token);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await refreshTokenManager.RemoveRefreshToken(context.Token);
            }
        }
    }
}