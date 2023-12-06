using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_FactRawMaterialMeasurement
    {
        public DateTime? FactLoadTs { get; set; }
        [StringLength(50)]
        public string DataSource { get; set; }
        public int? DimYearKey { get; set; }
        public long? DimDateKey { get; set; }
        public long DimMeasurementKey { get; set; }
        public long? DimRawMaterialKey { get; set; }
        public long DimFeatureKey { get; set; }
        public long DimAssetKey { get; set; }
        public long DimUOMKey { get; set; }
        public short FeatureCode { get; set; }
        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }
        public short AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        [StringLength(50)]
        public string ParentAssetName { get; set; }
        public short PassNo { get; set; }
        public bool IsLastPass { get; set; }
        public bool IsValid { get; set; }
        public double? ActualLength { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime MeasurementTime { get; set; }
        public double MeasurementValue { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
    }
}
