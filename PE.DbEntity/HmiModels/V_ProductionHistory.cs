using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ProductionHistory
    {
        public long ProductId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductCreated { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public double ProductWeight { get; set; }
        public short EnumQuality { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public short WorkOrderStatus { get; set; }
        public long ProductCatalogueId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        public long FKShapeId { get; set; }
        [Required]
        [StringLength(10)]
        public string ShapeCode { get; set; }
        [StringLength(50)]
        public string ShapeName { get; set; }
        public double? Thickness { get; set; }
        public double? Width { get; set; }
        public long SteelgradeId { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public long HeatId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public long ProductCatalogueTypeId { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductCatalogueTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueTypeName { get; set; }
        public int? NumDefects { get; set; }
    }
}
