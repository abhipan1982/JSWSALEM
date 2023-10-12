using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.BaseDbEntity.TransferModels
{
    [Keyless]
    [Table("ProductCatalogueTT", Schema = "xfr")]
    public partial class ProductCatalogueTT
    {
        [StringLength(255)]
        public string ProductCatalogueName { get; set; }
        public short? IsActive { get; set; }
        public short? IsDefault { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string SAPNumber { get; set; }
        public double? Length { get; set; }
        public double? LengthMin { get; set; }
        public double? LengthMax { get; set; }
        public double? Thickness { get; set; }
        public double? ThicknessMin { get; set; }
        public double? ThicknessMax { get; set; }
        public double? Width { get; set; }
        public double? WidthMin { get; set; }
        public double? WidthMax { get; set; }
        public double? Weight { get; set; }
        public double? WeightMin { get; set; }
        public double? WeightMax { get; set; }
        public double? Slitting { get; set; }
        public double? ExitSpeed { get; set; }
        public double? StdMetallicYield { get; set; }
        public double? StdQualityYield { get; set; }
        public double? StdGapTime { get; set; }
        public double? StdRollingTime { get; set; }
        public double? StdUtilizationTime { get; set; }
        public double? StdProductionTime { get; set; }
        public double? StdProductivity { get; set; }
        public double? MaxTensile { get; set; }
        public double? MaxYieldPoint { get; set; }
        public double? Ovality { get; set; }
        public double? MaxOvality { get; set; }
        public double? ProfileToleranceMin { get; set; }
        public double? ProfileToleranceMax { get; set; }
        [StringLength(255)]
        public string ShapeSymbol { get; set; }
        [StringLength(255)]
        public string SteelgradeCode { get; set; }
        [StringLength(255)]
        public string ProductCatalogueTypeSymbol { get; set; }
    }
}
