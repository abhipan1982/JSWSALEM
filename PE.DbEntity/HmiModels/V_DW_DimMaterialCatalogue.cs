using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimMaterialCatalogue
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(200)]
        public string MaterialCatalogueDescription { get; set; }
        [Required]
        [StringLength(10)]
        public string MaterialCatalogueTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueTypeName { get; set; }
        [Required]
        [StringLength(10)]
        public string MaterialShapeCode { get; set; }
        [StringLength(50)]
        public string MaterialShapeName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueLengthMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueWidthMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueThicknessMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueWeightMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueLengthMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueWidthMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueThicknessMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? MaterialCatalogueWeightMax { get; set; }
    }
}
