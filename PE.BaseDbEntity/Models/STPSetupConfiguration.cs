using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class STPSetupConfiguration
    {
        [Key]
        public long SetupConfigurationId { get; set; }
        public long FKConfigurationId { get; set; }
        public long FKSetupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SetupConfigurationLastSentTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKConfigurationId))]
        [InverseProperty(nameof(STPConfiguration.STPSetupConfigurations))]
        public virtual STPConfiguration FKConfiguration { get; set; }
        [ForeignKey(nameof(FKSetupId))]
        [InverseProperty(nameof(STPSetup.STPSetupConfigurations))]
        public virtual STPSetup FKSetup { get; set; }
    }
}
