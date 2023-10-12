using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AreaRawMaterialMeasurement
    {
        [StringLength(50)]
        public string AreaName { get; set; }
        public long AssetId { get; set; }
        public long OrderSeq { get; set; }
        public int AssetCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetName { get; set; }
        public long FeatureId { get; set; }
        public int FeatureCode { get; set; }
        [Required]
        [StringLength(75)]
        public string FeatureName { get; set; }
        public long UnitOfMeasureId { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitSymbol { get; set; }
        public long UnitCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public long? RawMaterialId { get; set; }
        public long? MeasurementId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MeasurementCreatedTs { get; set; }
        public double? MeasurementValueAvg { get; set; }
    }
}
