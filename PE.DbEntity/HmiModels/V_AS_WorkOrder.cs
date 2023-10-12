using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AS_WorkOrder
    {
        public long DimWorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string DimWorkOrderName { get; set; }
        [StringLength(4000)]
        public string DimYear { get; set; }
        [StringLength(4000)]
        public string DimMonth { get; set; }
        [Required]
        [StringLength(4000)]
        public string DimWeek { get; set; }
        [StringLength(4000)]
        public string DimDate { get; set; }
        [StringLength(10)]
        public string DimShiftCode { get; set; }
        [StringLength(51)]
        public string DimShiftKey { get; set; }
        [StringLength(50)]
        public string DimCrewName { get; set; }
        [Required]
        [StringLength(50)]
        public string DimMaterialCatalogueName { get; set; }
        [Required]
        [StringLength(50)]
        public string DimProductCatalogueName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string DimProductThickness { get; set; }
        [StringLength(50)]
        public string DimSteelgradeName { get; set; }
        [StringLength(50)]
        public string DimHeatName { get; set; }
        public double TargetWeight { get; set; }
        public int? MaterialsNumber { get; set; }
        public double? MaterialsWeight { get; set; }
        public int RawMaterialsNumber { get; set; }
        public double RawMaterialsWeight { get; set; }
        public int ProductsNumber { get; set; }
        public double ProductsWeight { get; set; }
        public int MaterialsScrappedNumber { get; set; }
        public double MaterialsScrappedWeight { get; set; }
        public long ProductionTime { get; set; }
        public long? RollingTime { get; set; }
        public long DelayDuration { get; set; }
        public double? MetallicYield { get; set; }
        public double? QualityYield { get; set; }
    }
}
