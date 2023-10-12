using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class TRKLayerRawMaterialRelation
    {
        [Key]
        public long ParentLayerRawMaterialId { get; set; }
        [Key]
        public long ChildLayerRawMaterialId { get; set; }
        public bool IsActualRelation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ChildLayerRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKLayerRawMaterialRelationChildLayerRawMaterials))]
        public virtual TRKRawMaterial ChildLayerRawMaterial { get; set; }
        [ForeignKey(nameof(ParentLayerRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKLayerRawMaterialRelationParentLayerRawMaterials))]
        public virtual TRKRawMaterial ParentLayerRawMaterial { get; set; }
    }
}
