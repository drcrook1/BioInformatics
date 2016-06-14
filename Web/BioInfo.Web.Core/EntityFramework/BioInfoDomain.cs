namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BioInfoDomain : DbContext
    {
        public BioInfoDomain()
            : base("name=BioInfoDomain")
        {
        }

        public virtual DbSet<Band> Bands { get; set; }
        public virtual DbSet<BandTelemtry> BandTelemtries { get; set; }
        public virtual DbSet<DomainUser> DomainUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BandTelemtry>()
                .Property(e => e.BandId)
                .IsFixedLength();

            modelBuilder.Entity<DomainUser>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<DomainUser>()
                .Property(e => e.Race)
                .IsFixedLength();

            modelBuilder.Entity<DomainUser>()
                .HasMany(e => e.BandTelemtries)
                .WithRequired(e => e.DomainUser)
                .WillCascadeOnDelete(false);
        }
    }
}
