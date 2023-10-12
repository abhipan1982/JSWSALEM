using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_RawMaterialLabel
    {
        public long RawMaterialId { get; set; }
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public short EnumWorkOrderStatus { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        public double Thickness { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialName { get; set; }
        public short MaterialSeqNo { get; set; }
    }
}
