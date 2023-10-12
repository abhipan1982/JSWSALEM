using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKFeatureId), nameof(SeqNo), nameof(TrackingInstructionValue), nameof(EnumTrackingInstructionType), nameof(FKAreaAssetId), nameof(FKPointAssetId), Name = "UQ_TrackingInstruction", IsUnique = true)]
    public partial class TRKTrackingInstruction
    {
        public TRKTrackingInstruction()
        {
            InverseFKParentTrackingInstruction = new HashSet<TRKTrackingInstruction>();
        }

        [Key]
        public long TrackingInstructionId { get; set; }
        public long FKFeatureId { get; set; }
        public long FKAreaAssetId { get; set; }
        public long? FKPointAssetId { get; set; }
        public long? FKParentTrackingInstructionId { get; set; }
        public short SeqNo { get; set; }
        public short? TrackingInstructionValue { get; set; }
        public TrackingInstructionType EnumTrackingInstructionType { get; set; }
        public short ChannelId { get; set; }
        public double? TimeFilter { get; set; }
        public bool IsAsync { get; set; }
        public bool IsIgnoredIfSimulation { get; set; }
        [Required]
        public bool? IsProcessedDuringAdjustment { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAreaAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKTrackingInstructionFKAreaAssets))]
        public virtual MVHAsset FKAreaAsset { get; set; }
        [ForeignKey(nameof(FKFeatureId))]
        [InverseProperty(nameof(MVHFeature.TRKTrackingInstructions))]
        public virtual MVHFeature FKFeature { get; set; }
        [ForeignKey(nameof(FKParentTrackingInstructionId))]
        [InverseProperty(nameof(TRKTrackingInstruction.InverseFKParentTrackingInstruction))]
        public virtual TRKTrackingInstruction FKParentTrackingInstruction { get; set; }
        [ForeignKey(nameof(FKPointAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKTrackingInstructionFKPointAssets))]
        public virtual MVHAsset FKPointAsset { get; set; }
        [InverseProperty(nameof(TRKTrackingInstruction.FKParentTrackingInstruction))]
        public virtual ICollection<TRKTrackingInstruction> InverseFKParentTrackingInstruction { get; set; }
    }
}
