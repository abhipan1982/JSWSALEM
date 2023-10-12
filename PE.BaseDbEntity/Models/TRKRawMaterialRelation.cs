using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class TRKRawMaterialRelation
    {
        [Key]
        public long ParentRawMaterialId { get; set; }
        [Key]
        public long ChildRawMaterialId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ChildRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialRelationChildRawMaterials))]
        public virtual TRKRawMaterial ChildRawMaterial { get; set; }
        [ForeignKey(nameof(ParentRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialRelationParentRawMaterials))]
        public virtual TRKRawMaterial ParentRawMaterial { get; set; }
    }
}
