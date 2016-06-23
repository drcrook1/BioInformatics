namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BioInfoDBContext : DbContext
    {
        public BioInfoDBContext()
            : base("name=BioInfoDBContext")
        {
        }

        public BioInfoDBContext(string connectionString)
           : base(connectionString)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Band> Bands { get; set; }
        public virtual DbSet<BandExperiment> BandExperiments { get; set; }
        public virtual DbSet<BandTelemetry> BandTelemetries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DomainUser> DomainUsers { get; set; }
        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Band>()
                .HasMany(e => e.BandExperiments)
                .WithRequired(e => e.Band)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BandTelemetry>()
                .Property(e => e.BandId)
                .IsFixedLength();

            modelBuilder.Entity<BandTelemetry>()
                .HasMany(e => e.BandExperiments)
                .WithRequired(e => e.BandTelemetry)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomainUser>()
                .Property(e => e.Gender)
                .IsFixedLength();

            modelBuilder.Entity<DomainUser>()
                .Property(e => e.Race)
                .IsFixedLength();

            modelBuilder.Entity<DomainUser>()
                .HasMany(e => e.BandExperiments)
                .WithRequired(e => e.DomainUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomainUser>()
                .HasMany(e => e.BandTelemetries)
                .WithOptional(e => e.DomainUser)
                .HasForeignKey(e => e.SkinTemperature);

            modelBuilder.Entity<Experiment>()
                .Property(e => e.ExperimentType)
                .IsFixedLength();

            modelBuilder.Entity<Experiment>()
                .HasMany(e => e.BandExperiments)
                .WithRequired(e => e.Experiment)
                .WillCascadeOnDelete(false);
        }
    }
}
