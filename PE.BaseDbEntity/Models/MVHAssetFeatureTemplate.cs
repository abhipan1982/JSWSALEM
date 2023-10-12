using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MVHAssetFeatureTemplate
    {
        [Key]
        public long AssetFeatureTemplateId { get; set; }
        public long FKAssetTemplateId { get; set; }
        public long FKFeatureTemplateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetTemplateId))]
        [InverseProperty(nameof(MVHAssetTemplate.MVHAssetFeatureTemplates))]
        public virtual MVHAssetTemplate FKAssetTemplate { get; set; }
        [ForeignKey(nameof(FKFeatureTemplateId))]
        [InverseProperty(nameof(MVHFeatureTemplate.MVHAssetFeatureTemplates))]
        public virtual MVHFeatureTemplate FKFeatureTemplate { get; set; }
    }
}
