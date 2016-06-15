namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BandExperiment")]
    public partial class BandExperiment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ExperimentId { get; set; }

        public int BandId { get; set; }

        public int BandTelemetryId { get; set; }

        public int DomainUserId { get; set; }

        public virtual Band Band { get; set; }

        public virtual Experiment Experiment { get; set; }

        public virtual BandTelemetry BandTelemetry { get; set; }

        public virtual DomainUser DomainUser { get; set; }
    }
}
