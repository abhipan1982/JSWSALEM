using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_ProductOverview
    {
        public long ProductId { get; set; }
        public bool IsDummy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductCreatedTs { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public bool IsAssigned { get; set; }
        public double ProductWeight { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(50)]
        public string ExternalProductCatalogueName { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        public short EnumInspectionResult { get; set; }
        public bool HasHeatChemicalAnalysis { get; set; }
    }
}
