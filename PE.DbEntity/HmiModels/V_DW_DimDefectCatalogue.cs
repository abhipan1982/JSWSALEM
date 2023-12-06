using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimDefectCatalogue
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimDefectCatalogueKey { get; set; }
        [Required]
        [StringLength(10)]
        public string DefectCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DefectCatalogueName { get; set; }
        [StringLength(100)]
        public string DefectCatalogueDescription { get; set; }
        public long? DimParentDefectCatalogueKey { get; set; }
        [StringLength(20)]
        public string DefectCategoryCode { get; set; }
        [StringLength(50)]
        public string DefectCategoryName { get; set; }
        [StringLength(100)]
        public string DefectCategoryDescription { get; set; }
    }
}
