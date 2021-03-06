namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BioInfoDBContext : DbContext
    {
       
        public BioInfoDBContext(string connectionString)
           : base(connectionString)
        {
#if DEBUG
            Database.SetInitializer(new BioInfoDBInitializer());
#endif
        }
        public virtual DbSet<Band> Bands { get; set; }
        public virtual DbSet<BandExperiment> BandExperiments { get; set; }
        public virtual DbSet<BandTelemetry> BandTelemetries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DomainUser> DomainUsers { get; set; }
        public virtual DbSet<Experiment> Experiments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

    public class BioInfoDBInitializer : DropCreateDatabaseAlways<BioInfoDBContext>
    {
        protected override void Seed(BioInfoDBContext context)
        {

            var dUser = new DomainUser()
            {
                Id = 1,
                First_Name = "First",
                Last_Name = "Last",
                Gender = "Alien", //why are these required?
                Race = "Super", //why are these required?,
                BirthDate = DateTime.Now.AddYears(-20)
            };

            context.DomainUsers.Add(dUser);

            base.Seed(context);
        }
    }
}
