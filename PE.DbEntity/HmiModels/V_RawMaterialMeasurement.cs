using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialMeasurement
    {
        public long MeasurementId { get; set; }
        public long RawMaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public long? MaterialId { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? HeatId { get; set; }
        public long? SteelgradeId { get; set; }
        public long FeatureId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        [StringLength(100)]
        public string FeatureDescription { get; set; }
        public bool IsSampledFeature { get; set; }
        public long AssetId { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public int? AreaCode { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public int? ZoneCode { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        public bool IsValid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MeasurementTime { get; set; }
        public double? MeasurementValueMin { get; set; }
        public double MeasurementValueAvg { get; set; }
        public double? MeasurementValueMax { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
    }
}
