using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FeatureTemplateName), Name = "UQ_FeatureTemplateName", IsUnique = true)]
    public partial class MVHFeatureTemplate
    {
        public MVHFeatureTemplate()
        {
            MVHAssetFeatureTemplates = new HashSet<MVHAssetFeatureTemplate>();
        }

        [Key]
        public long FeatureTemplateId { get; set; }
        public long FKUnitOfMeasureId { get; set; }
        public long FKExtUnitOfMeasureId { get; set; }
        public long FKDataTypeId { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureTemplateName { get; set; }
        [StringLength(100)]
        public string FeatureTemplateDescription { get; set; }
        public FeatureType EnumFeatureType { get; set; }
        public CommChannelType EnumCommChannelType { get; set; }
        public AggregationStrategy EnumAggregationStrategy { get; set; }
        [StringLength(350)]
        public string TemplateCommAttr1 { get; set; }
        [StringLength(350)]
        public string TemplateCommAttr2 { get; set; }
        [StringLength(350)]
        public string TemplateCommAttr3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKDataTypeId))]
        [InverseProperty(nameof(DBDataType.MVHFeatureTemplates))]
        public virtual DBDataType FKDataType { get; set; }
        [InverseProperty(nameof(MVHAssetFeatureTemplate.FKFeatureTemplate))]
        public virtual ICollection<MVHAssetFeatureTemplate> MVHAssetFeatureTemplates { get; set; }
    }
}
