using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class TRKLayerRelation
    {
        [Key]
        public long ParentLayerId { get; set; }
        [Key]
        public long ChildLayerId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ChildLayerId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKLayerRelationChildLayers))]
        public virtual TRKRawMaterial ChildLayer { get; set; }
        [ForeignKey(nameof(ParentLayerId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKLayerRelationParentLayers))]
        public virtual TRKRawMaterial ParentLayer { get; set; }
    }
}
