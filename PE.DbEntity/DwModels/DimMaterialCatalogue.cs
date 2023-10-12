using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimMaterialCatalogue
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimMaterialCatalogueRow { get; set; }
        public bool DimMaterialCatalogueIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimMaterialCatalogueHash { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        public long DimMaterialTypeKey { get; set; }
        public long DimMaterialShapeKey { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(200)]
        public string MaterialCatalogueDescription { get; set; }
        [StringLength(50)]
        public string ExternalMaterialCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string MaterialTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialTypeName { get; set; }
        [Required]
        [StringLength(10)]
        public string MaterialShapeCode { get; set; }
        [StringLength(50)]
        public string MaterialShapeName { get; set; }
        public double? MaterialCatalogueLengthMin { get; set; }
        public double? MaterialCatalogueLengthMax { get; set; }
        public double MaterialCatalogueThicknessMin { get; set; }
        public double MaterialCatalogueThicknessMax { get; set; }
        public double? MaterialCatalogueWidthMin { get; set; }
        public double? MaterialCatalogueWidthMax { get; set; }
        public double? MaterialCatalogueWeightMin { get; set; }
        public double? MaterialCatalogueWeightMax { get; set; }
    }
}
