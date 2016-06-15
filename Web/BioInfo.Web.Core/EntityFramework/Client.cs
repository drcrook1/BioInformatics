namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        public string Id { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ApplicationType { get; set; }

        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [StringLength(100)]
        public string AllowedOrigin { get; set; }
    }
}
