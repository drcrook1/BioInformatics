namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BandTelemetry")]
    public partial class BandTelemetry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BandTelemetry()
        {
            BandExperiments = new HashSet<BandExperiment>();
        }

        public int Id { get; set; }

        public DateTime CollectionTime { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public double? BarometricPressure { get; set; }

        public double? Capacitance { get; set; }

        public double? HeartRate { get; set; }

        public double? AmbientLight { get; set; }

        public int? SkinTemperature { get; set; }

        [Required]
        [StringLength(10)]
        public string BandId { get; set; }

        public double? UV { get; set; }

        public double? GalvSkinRes { get; set; }

        public double? Accelerometer { get; set; }

        public double? Microphone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BandExperiment> BandExperiments { get; set; }

        public virtual DomainUser DomainUser { get; set; }
    }
}
