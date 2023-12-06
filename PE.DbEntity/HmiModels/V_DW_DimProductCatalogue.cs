using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimProductCatalogue
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimProductCatalogueKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(200)]
        public string ProductCatalogueDescription { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueTypeName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductShapeCode { get; set; }
        [StringLength(50)]
        public string ProductShapeName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueLength { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueWidth { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueThickness { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueLengthMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueWidthMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueThicknessMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueWeightMin { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueLengthMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueWidthMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueThicknessMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueWeightMax { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueExitSpeed { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueStdProductivity { get; set; }
        [Column(TypeName = "numeric(18, 8)")]
        public decimal? ProductCatalogueMaxOvality { get; set; }
    }
}
