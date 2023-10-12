using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class STPConfiguration
    {
        public STPConfiguration()
        {
            STPSetupConfigurations = new HashSet<STPSetupConfiguration>();
        }

        [Key]
        public long ConfigurationId { get; set; }
        [Required]
        [StringLength(50)]
        public string ConfigurationName { get; set; }
        [StringLength(400)]
        public string ConfigurationComment { get; set; }
        public short ConfigurationVersion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ConfigurationCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfigurationLastSentTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(STPSetupConfiguration.FKConfiguration))]
        public virtual ICollection<STPSetupConfiguration> STPSetupConfigurations { get; set; }
    }
}
