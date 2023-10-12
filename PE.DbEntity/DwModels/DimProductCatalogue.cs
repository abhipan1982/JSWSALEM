using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimProductCatalogue
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimProductCatalogueRow { get; set; }
        public bool DimProductCatalogueIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimProductCatalogueHash { get; set; }
        public long DimProductCatalogueKey { get; set; }
        public long DimProductTypeKey { get; set; }
        public long DimProductShapeKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(200)]
        public string ProductCatalogueDescription { get; set; }
        [StringLength(50)]
        public string ProductCatalogueExternalName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductTypeName { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductShapeCode { get; set; }
        [StringLength(50)]
        public string ProductShapeName { get; set; }
        public double? ProductCatalogueLength { get; set; }
        public double? ProductCatalogueLengthMin { get; set; }
        public double? ProductCatalogueLengthMax { get; set; }
        public double ProductCatalogueThickness { get; set; }
        public double ProductCatalogueThicknessMin { get; set; }
        public double ProductCatalogueThicknessMax { get; set; }
        public double? ProductCatalogueWidth { get; set; }
        public double? ProductCatalogueWidthMin { get; set; }
        public double? ProductCatalogueWidthMax { get; set; }
        public double? ProductCatalogueWeight { get; set; }
        public double? ProductCatalogueWeightMin { get; set; }
        public double? ProductCatalogueWeightMax { get; set; }
        public double? ProductCatalogueMaxOvality { get; set; }
        public double ProductCatalogueStdProductivity { get; set; }
        public double ProductCatalogueStdMetallicYield { get; set; }
    }
}
