using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_AS_Delay
    {
        public long DimDelayId { get; set; }
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
        public string DimDelayCatalogueCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DimDelayCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string DimDelayCatalogueCategoryCode { get; set; }
        [Required]
        [StringLength(50)]
        public string DimDelayCatalogueCategoryName { get; set; }
        public bool DimIsPlanned { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EventStartTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EventEndTs { get; set; }
        public double? DelayDuration { get; set; }
        public int IsOpen { get; set; }
    }
}
