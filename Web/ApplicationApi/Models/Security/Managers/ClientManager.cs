using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Managers
{
    public class ClientManager : IDisposable
    {
        internal bool _disposed;
        internal SecurityDBContext _dbContext;

        public ClientManager(SecurityDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static ClientManager Create(IdentityFactoryOptions<ClientManager> options, IOwinContext context)
        {
            var dbContext = context.Get<SecurityDBContext>();
            return new ClientManager(dbContext);
        }


        public Client FindClient(string clientId)
        {
            return _dbContext.Clients.Find(clientId);
        }

        public IList<Client> GetAllClients()
        {
            return _dbContext.Clients.ToList();
        }

        public async Task CeateClientAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.Entry(client).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
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