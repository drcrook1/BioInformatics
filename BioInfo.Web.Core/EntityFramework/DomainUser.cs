namespace BioInfo.Web.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DomainUser")]
    public partial class DomainUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DomainUser()
        {
            BandTelemtries = new HashSet<BandTelemtry>();
        }

        public int Id { get; set; }

        [Column("First Name")]
        [Required]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [Required]
        [StringLength(50)]
        public string Last_Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        public double HeightInches { get; set; }

        public double Weight { get; set; }

        [Required]
        [StringLength(10)]
        public string Race { get; set; }

        [Column("Current Medications")]
        [StringLength(255)]
        public string Current_Medications { get; set; }

        [Column("Medical History")]
        [StringLength(4000)]
        public string Medical_History { get; set; }

        public bool HasGlasses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BandTelemtry> BandTelemtries { get; set; }
    }
}
