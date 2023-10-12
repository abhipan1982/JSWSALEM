using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AS_Defect
    {
        public long DimDefectId { get; set; }
        [StringLength(4000)]
        public string DimYear { get; set; }
        [StringLength(4000)]
        public string DimMonth { get; set; }
        [Required]
        [StringLength(4000)]
        public string DimWeek { get; set; }
        [StringLength(4000)]
        public string DimDate { get; set; }
        [Required]
        [StringLength(10)]
        public string DimShiftCode { get; set; }
        [Required]
        [StringLength(51)]
        public string DimShiftKey { get; set; }
        [Required]
        [StringLength(50)]
        public string DimCrewName { get; set; }
        [Required]
        [StringLength(10)]
        public string DimDefectCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DimDefectCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string DimDefectCatalogueCategoryCode { get; set; }
        [StringLength(50)]
        public string DimDefectCatalogueCategoryName { get; set; }
        [StringLength(50)]
        public string DimRawMaterialName { get; set; }
        [StringLength(50)]
        public string DimMaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string DimProductCatalogueName { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string DimProductThickness { get; set; }
        [StringLength(50)]
        public string DimWorkOrderName { get; set; }
        [StringLength(50)]
        public string DimSteelgradeName { get; set; }
        [StringLength(50)]
        public string DimHeatName { get; set; }
        [StringLength(50)]
        public string DimAssetName { get; set; }
        public int DefectsNumber { get; set; }
    }
}
