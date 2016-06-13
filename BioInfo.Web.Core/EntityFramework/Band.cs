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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? DomainUserId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(255)]
        public string IoTHubKey { get; set; }

        [StringLength(255)]
        public string IoTHubId { get; set; }
    }
}
