using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Measurement
    {
        public long MeasurementId { get; set; }
        public long? RawMaterialId { get; set; }
        public long FeatureId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long AssetId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
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
