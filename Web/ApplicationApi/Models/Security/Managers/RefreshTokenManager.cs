using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Managers
{
    public class RefreshTokenManager : IDisposable
    {
        internal bool _disposed;
        internal SecurityDBContext _dbContext;

        public RefreshTokenManager(SecurityDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static RefreshTokenManager Create(IdentityFactoryOptions<RefreshTokenManager> options, IOwinContext context)
        {
            var dbContext = context.Get<SecurityDBContext>();
            return new RefreshTokenManager(dbContext);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _dbContext.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                await RemoveRefreshToken(existingToken);
            }

            _dbContext.RefreshTokens.Add(token);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _dbContext.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken == null) return false;

            _dbContext.RefreshTokens.Remove(refreshToken);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _dbContext.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _dbContext.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}