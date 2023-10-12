using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DefectsSummary
    {
        public long DefectId { get; set; }
        public long DefectCatalogueId { get; set; }
        public long? RawMaterialId { get; set; }
        public long? ProductId { get; set; }
        [StringLength(50)]
        public string DefectName { get; set; }
        public short? DefectPosition { get; set; }
        public short? DefectFrequency { get; set; }
        public short? DefectScale { get; set; }
        [StringLength(200)]
        public string DefectDescription { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DefectCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string DefectCatalogueCategoryName { get; set; }
        public bool? IsRawMaterialRelated { get; set; }
        public bool? IsProductRelated { get; set; }
        [StringLength(50)]
        public string RawMaterialName { get; set; }
        [StringLength(50)]
        public string MaterialName { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? AssetId { get; set; }
        [StringLength(50)]
        public string AssetName { get; set; }
    }
}
