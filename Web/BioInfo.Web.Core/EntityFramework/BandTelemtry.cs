namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BandTelemtry")]
    public partial class BandTelemtry
    {
        public int Id { get; set; }

        public DateTime CollectionTime { get; set; }

        public double? Barometer { get; set; }

        public double? HeartRate { get; set; }

        public double? UV { get; set; }

        public double? Microphone { get; set; }

        public double? GalvSkinRes { get; set; }

        public double? Acceleromteter { get; set; }

        public int DomainUserId { get; set; }

        [StringLength(10)]
        public string BandId { get; set; }

        public virtual DomainUser DomainUser { get; set; }
    }
}
