using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WorkOrderLocation
    {
        public long OrderSeq { get; set; }
        public int? AreaCode { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public long? WorkOrderId { get; set; }
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long? HeatId { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        public long? SteelgradeId { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        public int? RawMaterialNumber { get; set; }
    }
}
