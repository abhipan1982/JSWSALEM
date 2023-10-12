using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKSetupTypeId), nameof(FKInstructionId), nameof(FKAssetId), Name = "UQ_SetupTypeId_InstructionId_AssetId", IsUnique = true)]
    [Index(nameof(FKSetupTypeId), nameof(OrderSeq), Name = "UQ_SetupTypeId_OrderSeq", IsUnique = true)]
    public partial class STPSetupTypeInstruction
    {
        public STPSetupTypeInstruction()
        {
            STPSetupInstructions = new HashSet<STPSetupInstruction>();
        }

        [Key]
        public long SetupTypeInstructionId { get; set; }
        public long FKSetupTypeId { get; set; }
        public long FKInstructionId { get; set; }
        public long? FKAssetId { get; set; }
        public short OrderSeq { get; set; }
        public double? RangeFrom { get; set; }
        public double? RangeTo { get; set; }
        [Required]
        public bool? IsRequired { get; set; }
        [Required]
        public bool? IsSentToL1 { get; set; }
        public CommChannelType EnumCommChannelType { get; set; }
        [StringLength(50)]
        public string CommAttr1 { get; set; }
        [StringLength(50)]
        public string CommAttr2 { get; set; }
        [StringLength(50)]
        public string CommAttr3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.STPSetupTypeInstructions))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKInstructionId))]
        [InverseProperty(nameof(STPInstruction.STPSetupTypeInstructions))]
        public virtual STPInstruction FKInstruction { get; set; }
        [ForeignKey(nameof(FKSetupTypeId))]
        [InverseProperty(nameof(STPSetupType.STPSetupTypeInstructions))]
        public virtual STPSetupType FKSetupType { get; set; }
        [InverseProperty(nameof(STPSetupInstruction.FKSetupTypeInstruction))]
        public virtual ICollection<STPSetupInstruction> STPSetupInstructions { get; set; }
    }
}
