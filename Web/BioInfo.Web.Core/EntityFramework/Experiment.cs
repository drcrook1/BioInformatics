namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Experiment")]
    public partial class Experiment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Experiment()
        {
            BandExperiments = new HashSet<BandExperiment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(4000)]
        public string StartCondition { get; set; }

        [StringLength(4000)]
        public string EndCondition { get; set; }

        [StringLength(4000)]
        public string Notes { get; set; }

        [StringLength(50)]
        public string ExperimentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BandExperiment> BandExperiments { get; set; }
    }
}
