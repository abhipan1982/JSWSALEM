using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class PRMProductStep
    {
        [Key]
        public long ProductStepId { get; set; }
        public long FKProductId { get; set; }
        public long FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StepCreatedTs { get; set; }
        public short StepNo { get; set; }
        public short PositionX { get; set; }
        public short PositionY { get; set; }
        public short GroupNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.PRMProductSteps))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKProductId))]
        [InverseProperty(nameof(PRMProduct.PRMProductSteps))]
        public virtual PRMProduct FKProduct { get; set; }
    }
}
