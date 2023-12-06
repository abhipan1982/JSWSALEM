using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimDelayCatalogue
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimDelayCatalogueKey { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventCatalogueName { get; set; }
        [StringLength(100)]
        public string EventCatalogueDescription { get; set; }
        public double DelayStdTime { get; set; }
        public long? DimParentDelayCatalogueKey { get; set; }
        [StringLength(10)]
        public string DelayCategoryCode { get; set; }
        [StringLength(50)]
        public string DelayCategoryName { get; set; }
        [StringLength(100)]
        public string DelayCategoryDescription { get; set; }
    }
}
