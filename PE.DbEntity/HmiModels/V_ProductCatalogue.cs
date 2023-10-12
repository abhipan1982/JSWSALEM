using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ProductCatalogue
    {
        public long ProductCatalogueId { get; set; }
        public long ProductCatalogueTypeID { get; set; }
        public long shapeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(200)]
        public string ProductCatalogueDescription { get; set; }
        [StringLength(50)]
        public string ExternalProductCatalogueName { get; set; }
        public double? Length { get; set; }
        public double? LengthMin { get; set; }
        public double? LengthMax { get; set; }
        public double Thickness { get; set; }
        public double ThicknessMin { get; set; }
        public double ThicknessMax { get; set; }
        public double? Width { get; set; }
        public double? WidthMin { get; set; }
        public double? WidthMax { get; set; }
        public double? Weight { get; set; }
        public double? WeightMin { get; set; }
        public double? WeightMax { get; set; }
        public double? MaxOvality { get; set; }
        public double StdProductivity { get; set; }
        public double StdMetallicYield { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
        public long FKProductCatalogueId { get; set; }
        public double? MinOvality { get; set; }
        public double? MinDiameter { get; set; }
        public double? MaxDiameter { get; set; }
        public double? Diameter { get; set; }
        public double? NegRcsSide { get; set; }
        public double? PosRcsSide { get; set; }
        public double? MinSquareness { get; set; }
        public double? MaxSquareness { get; set; }
        [StringLength(50)]
        public string ShapeName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
    }
}
