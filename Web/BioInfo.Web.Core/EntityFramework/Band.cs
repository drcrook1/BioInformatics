namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Band")]
    public partial class Band
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Band()
        {
            BandExperiments = new HashSet<BandExperiment>();
        }

        public Band(string ioTHubName, int userId)
        {
            this.IoTHubId = ioTHubName;
            this.DomainUserId = userId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? DomainUserId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(255)]
        public string IoTHubKey { get; set; }

        [StringLength(255)]
        public string IoTHubId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BandExperiment> BandExperiments { get; set; }
    }
}
