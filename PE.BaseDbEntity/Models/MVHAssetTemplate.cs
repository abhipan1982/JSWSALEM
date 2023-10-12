using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(AssetTemplateName), Name = "UQ_AssetTemplateName", IsUnique = true)]
    public partial class MVHAssetTemplate
    {
        public MVHAssetTemplate()
        {
            MVHAssetFeatureTemplates = new HashSet<MVHAssetFeatureTemplate>();
        }

        [Key]
        public long AssetTemplateId { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetTemplateName { get; set; }
        [StringLength(100)]
        public string AssetTemplateDescription { get; set; }
        public bool IsArea { get; set; }
        public bool IsZone { get; set; }
        public TrackingAreaType EnumTrackingAreaType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(MVHAssetFeatureTemplate.FKAssetTemplate))]
        public virtual ICollection<MVHAssetFeatureTemplate> MVHAssetFeatureTemplates { get; set; }
    }
}
