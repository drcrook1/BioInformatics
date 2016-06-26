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

        public SecurityDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SecurityDBContext Create()
        {
            return new SecurityDBContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}