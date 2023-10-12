using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MVHFeatureCustom
    {
        [Key]
        public long FeatureCustomId { get; set; }
        public long FKFeatureId { get; set; }
        public long FKLanguageId { get; set; }
        public long FKUnitOfMeasureId { get; set; }
        public long? FKUnitOfMeasureFormatId { get; set; }

        [ForeignKey(nameof(FKFeatureId))]
        [InverseProperty(nameof(MVHFeature.MVHFeatureCustoms))]
        public virtual MVHFeature FKFeature { get; set; }
    }
}
