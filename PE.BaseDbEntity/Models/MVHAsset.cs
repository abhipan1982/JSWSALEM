using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(AssetCode), Name = "UQ_AssetCode", IsUnique = true)]
    [Index(nameof(AssetName), Name = "UQ_AssetName", IsUnique = true)]
    [Index(nameof(OrderSeq), Name = "UQ_OrderSeq", IsUnique = true)]
    public partial class MVHAsset
    {
        public MVHAsset()
        {
            EVTEvents = new HashSet<EVTEvent>();
            InverseFKParentAsset = new HashSet<MVHAsset>();
            MNTEquipments = new HashSet<MNTEquipment>();
            MVHAssetLayoutFKNextAssets = new HashSet<MVHAssetLayout>();
            MVHAssetLayoutFKPrevAssets = new HashSet<MVHAssetLayout>();
            MVHFeatures = new HashSet<MVHFeature>();
            PRMMaterialSteps = new HashSet<PRMMaterialStep>();
            PRMProductSteps = new HashSet<PRMProductStep>();
            QETriggers = new HashSet<QETrigger>();
            QTYDefects = new HashSet<QTYDefect>();
            STPSetupTypeInstructions = new HashSet<STPSetupTypeInstruction>();
            TRKRawMaterialFKLastAssets = new HashSet<TRKRawMaterial>();
            TRKRawMaterialFKScrapAssets = new HashSet<TRKRawMaterial>();
            TRKRawMaterialLocationFKAssets = new HashSet<TRKRawMaterialLocation>();
            TRKRawMaterialLocationFKCtrAssets = new HashSet<TRKRawMaterialLocation>();
            TRKRawMaterialsCuts = new HashSet<TRKRawMaterialsCut>();
            TRKRawMaterialsSteps = new HashSet<TRKRawMaterialsStep>();
            TRKTrackingInstructionFKAreaAssets = new HashSet<TRKTrackingInstruction>();
            TRKTrackingInstructionFKPointAssets = new HashSet<TRKTrackingInstruction>();
            ZPCZebraPrinters = new HashSet<ZPCZebraPrinter>();
        }

        [Key]
        public long AssetId { get; set; }
        public int OrderSeq { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(100)]
        public string AssetDescription { get; set; }
        public long? FKParentAssetId { get; set; }
        public long? FKAssetTypeId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsArea { get; set; }
        public bool IsZone { get; set; }
        [Required]
        public bool? IsTrackingPoint { get; set; }
        public bool IsDelayCheckpoint { get; set; }
        public bool IsReversible { get; set; }
        public bool IsVisibleOnMVH { get; set; }
        public bool IsPositionBased { get; set; }
        public short? PositionsNumber { get; set; }
        public short? VirtualPositionsNumber { get; set; }
        public TrackingAreaType EnumTrackingAreaType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetTypeId))]
        [InverseProperty(nameof(MVHAssetType.MVHAssets))]
        public virtual MVHAssetType FKAssetType { get; set; }
        [ForeignKey(nameof(FKParentAssetId))]
        [InverseProperty(nameof(MVHAsset.InverseFKParentAsset))]
        public virtual MVHAsset FKParentAsset { get; set; }
        [InverseProperty("FKAsset")]
        public virtual MVHAssetsLocation MVHAssetsLocation { get; set; }
        [InverseProperty(nameof(EVTEvent.FKAsset))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(MVHAsset.FKParentAsset))]
        public virtual ICollection<MVHAsset> InverseFKParentAsset { get; set; }
        [InverseProperty(nameof(MNTEquipment.FKAsset))]
        public virtual ICollection<MNTEquipment> MNTEquipments { get; set; }
        [InverseProperty(nameof(MVHAssetLayout.FKNextAsset))]
        public virtual ICollection<MVHAssetLayout> MVHAssetLayoutFKNextAssets { get; set; }
        [InverseProperty(nameof(MVHAssetLayout.FKPrevAsset))]
        public virtual ICollection<MVHAssetLayout> MVHAssetLayoutFKPrevAssets { get; set; }
        [InverseProperty(nameof(MVHFeature.FKAsset))]
        public virtual ICollection<MVHFeature> MVHFeatures { get; set; }
        [InverseProperty(nameof(PRMMaterialStep.FKAsset))]
        public virtual ICollection<PRMMaterialStep> PRMMaterialSteps { get; set; }
        [InverseProperty(nameof(PRMProductStep.FKAsset))]
        public virtual ICollection<PRMProductStep> PRMProductSteps { get; set; }
        [InverseProperty(nameof(QETrigger.FKAsset))]
        public virtual ICollection<QETrigger> QETriggers { get; set; }
        [InverseProperty(nameof(QTYDefect.FKAsset))]
        public virtual ICollection<QTYDefect> QTYDefects { get; set; }
        [InverseProperty(nameof(STPSetupTypeInstruction.FKAsset))]
        public virtual ICollection<STPSetupTypeInstruction> STPSetupTypeInstructions { get; set; }
        [InverseProperty(nameof(TRKRawMaterial.FKLastAsset))]
        public virtual ICollection<TRKRawMaterial> TRKRawMaterialFKLastAssets { get; set; }
        [InverseProperty(nameof(TRKRawMaterial.FKScrapAsset))]
        public virtual ICollection<TRKRawMaterial> TRKRawMaterialFKScrapAssets { get; set; }
        [InverseProperty(nameof(TRKRawMaterialLocation.FKAsset))]
        public virtual ICollection<TRKRawMaterialLocation> TRKRawMaterialLocationFKAssets { get; set; }
        [InverseProperty(nameof(TRKRawMaterialLocation.FKCtrAsset))]
        public virtual ICollection<TRKRawMaterialLocation> TRKRawMaterialLocationFKCtrAssets { get; set; }
        [InverseProperty(nameof(TRKRawMaterialsCut.FKAsset))]
        public virtual ICollection<TRKRawMaterialsCut> TRKRawMaterialsCuts { get; set; }
        [InverseProperty(nameof(TRKRawMaterialsStep.FKAsset))]
        public virtual ICollection<TRKRawMaterialsStep> TRKRawMaterialsSteps { get; set; }
        [InverseProperty(nameof(TRKTrackingInstruction.FKAreaAsset))]
        public virtual ICollection<TRKTrackingInstruction> TRKTrackingInstructionFKAreaAssets { get; set; }
        [InverseProperty(nameof(TRKTrackingInstruction.FKPointAsset))]
        public virtual ICollection<TRKTrackingInstruction> TRKTrackingInstructionFKPointAssets { get; set; }
        [InverseProperty(nameof(ZPCZebraPrinter.FKAsset))]
        public virtual ICollection<ZPCZebraPrinter> ZPCZebraPrinters { get; set; }
    }
}
