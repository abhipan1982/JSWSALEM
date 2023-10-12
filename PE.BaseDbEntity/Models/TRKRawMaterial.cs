using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKMaterialId), Name = "NCI_MaterialId")]
    [Index(nameof(FKProductId), Name = "NCI_ProductId")]
    [Index(nameof(FKShiftCalendarId), Name = "NCI_ShiftCalendarId")]
    [Index(nameof(EnumRawMaterialStatus), Name = "NCI_Status")]
    [Index(nameof(RawMaterialId), nameof(CuttingSeqNo), Name = "UQ_CuttingSeqNo", IsUnique = true)]
    [Index(nameof(RawMaterialName), Name = "UQ_RawMaterialName", IsUnique = true)]
    public partial class TRKRawMaterial
    {
        public TRKRawMaterial()
        {
            EVTEvents = new HashSet<EVTEvent>();
            MVHMeasurements = new HashSet<MVHMeasurement>();
            QERuleMappingValues = new HashSet<QERuleMappingValue>();
            QTYDefects = new HashSet<QTYDefect>();
            TRKLayerRawMaterialRelationChildLayerRawMaterials = new HashSet<TRKLayerRawMaterialRelation>();
            TRKLayerRawMaterialRelationParentLayerRawMaterials = new HashSet<TRKLayerRawMaterialRelation>();
            TRKLayerRelationChildLayers = new HashSet<TRKLayerRelation>();
            TRKLayerRelationParentLayers = new HashSet<TRKLayerRelation>();
            TRKRawMaterialLocations = new HashSet<TRKRawMaterialLocation>();
            TRKRawMaterialRelationChildRawMaterials = new HashSet<TRKRawMaterialRelation>();
            TRKRawMaterialRelationParentRawMaterials = new HashSet<TRKRawMaterialRelation>();
            TRKRawMaterialsCuts = new HashSet<TRKRawMaterialsCut>();
            TRKRawMaterialsInFurnaces = new HashSet<TRKRawMaterialsInFurnace>();
            TRKRawMaterialsSteps = new HashSet<TRKRawMaterialsStep>();
        }

        [Key]
        public long RawMaterialId { get; set; }
        public long FKShiftCalendarId { get; set; }
        public long? FKLastAssetId { get; set; }
        public long? FKScrapAssetId { get; set; }
        public long? FKMaterialId { get; set; }
        public long? FKProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [StringLength(50)]
        public string OldRawMaterialName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RawMaterialCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RawMaterialEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollingStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RollingEndTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProductCreatedTs { get; set; }
        public bool IsDummy { get; set; }
        public bool IsVirtual { get; set; }
        public RawMaterialType EnumRawMaterialType { get; set; }
        public RawMaterialStatus EnumRawMaterialStatus { get; set; }
        public LayerStatus EnumLayerStatus { get; set; }
        public ChargeType EnumChargeType { get; set; }
        public RejectLocation EnumRejectLocation { get; set; }
        public TypeOfScrap EnumTypeOfScrap { get; set; }
        public GradingSource EnumGradingSource { get; set; }
        public CuttingTip EnumCuttingTip { get; set; }
        public short CuttingSeqNo { get; set; }
        public short ChildsNo { get; set; }
        public short OutputPieces { get; set; }
        public short SlittingFactor { get; set; }
        public double? LastWeight { get; set; }
        public double? LastLength { get; set; }
        public double? LastTemperature { get; set; }
        public double? LastGrading { get; set; }
        public double? WeighingStationWeight { get; set; }
        public double? ScrapPercent { get; set; }
        [StringLength(200)]
        public string ScrapRemarks { get; set; }
        public double? FurnaceExitTemperature { get; set; }
        public int? FurnaceHeatingDuration { get; set; }
        public bool IsAfterDelayPoint { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKLastAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialFKLastAssets))]
        public virtual MVHAsset FKLastAsset { get; set; }
        [ForeignKey(nameof(FKMaterialId))]
        [InverseProperty(nameof(PRMMaterial.TRKRawMaterials))]
        public virtual PRMMaterial FKMaterial { get; set; }
        [ForeignKey(nameof(FKProductId))]
        [InverseProperty(nameof(PRMProduct.TRKRawMaterials))]
        public virtual PRMProduct FKProduct { get; set; }
        [ForeignKey(nameof(FKScrapAssetId))]
        [InverseProperty(nameof(MVHAsset.TRKRawMaterialFKScrapAssets))]
        public virtual MVHAsset FKScrapAsset { get; set; }
        [ForeignKey(nameof(FKShiftCalendarId))]
        [InverseProperty(nameof(EVTShiftCalendar.TRKRawMaterials))]
        public virtual EVTShiftCalendar FKShiftCalendar { get; set; }
        [InverseProperty("FKRawMaterial")]
        public virtual QTYQualityInspection QTYQualityInspection { get; set; }
        [InverseProperty(nameof(EVTEvent.FKRawMaterial))]
        public virtual ICollection<EVTEvent> EVTEvents { get; set; }
        [InverseProperty(nameof(MVHMeasurement.FKRawMaterial))]
        public virtual ICollection<MVHMeasurement> MVHMeasurements { get; set; }
        [InverseProperty(nameof(QERuleMappingValue.FKRawMaterial))]
        public virtual ICollection<QERuleMappingValue> QERuleMappingValues { get; set; }
        [InverseProperty(nameof(QTYDefect.FKRawMaterial))]
        public virtual ICollection<QTYDefect> QTYDefects { get; set; }
        [InverseProperty(nameof(TRKLayerRawMaterialRelation.ChildLayerRawMaterial))]
        public virtual ICollection<TRKLayerRawMaterialRelation> TRKLayerRawMaterialRelationChildLayerRawMaterials { get; set; }
        [InverseProperty(nameof(TRKLayerRawMaterialRelation.ParentLayerRawMaterial))]
        public virtual ICollection<TRKLayerRawMaterialRelation> TRKLayerRawMaterialRelationParentLayerRawMaterials { get; set; }
        [InverseProperty(nameof(TRKLayerRelation.ChildLayer))]
        public virtual ICollection<TRKLayerRelation> TRKLayerRelationChildLayers { get; set; }
        [InverseProperty(nameof(TRKLayerRelation.ParentLayer))]
        public virtual ICollection<TRKLayerRelation> TRKLayerRelationParentLayers { get; set; }
        [InverseProperty(nameof(TRKRawMaterialLocation.FKRawMaterial))]
        public virtual ICollection<TRKRawMaterialLocation> TRKRawMaterialLocations { get; set; }
        [InverseProperty(nameof(TRKRawMaterialRelation.ChildRawMaterial))]
        public virtual ICollection<TRKRawMaterialRelation> TRKRawMaterialRelationChildRawMaterials { get; set; }
        [InverseProperty(nameof(TRKRawMaterialRelation.ParentRawMaterial))]
        public virtual ICollection<TRKRawMaterialRelation> TRKRawMaterialRelationParentRawMaterials { get; set; }
        [InverseProperty(nameof(TRKRawMaterialsCut.FKRawMaterial))]
        public virtual ICollection<TRKRawMaterialsCut> TRKRawMaterialsCuts { get; set; }
        [InverseProperty(nameof(TRKRawMaterialsInFurnace.FKRawMaterial))]
        public virtual ICollection<TRKRawMaterialsInFurnace> TRKRawMaterialsInFurnaces { get; set; }
        [InverseProperty(nameof(TRKRawMaterialsStep.FKRawMaterial))]
        public virtual ICollection<TRKRawMaterialsStep> TRKRawMaterialsSteps { get; set; }
    }
}
