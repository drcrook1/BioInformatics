using BioInfo.Web.Core.EntityFramework;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.Managers
{
    public class SecurityDBContext : IdentityDbContext<ApplicationUser>
    {

        public SecurityDBContext(string DBConnString)
            : base(DBConnString, throwIfV1Schema: false)
        {
        }

        public static SecurityDBContext Create(string DBConnString)
        {
            return new SecurityDBContext(DBConnString);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}