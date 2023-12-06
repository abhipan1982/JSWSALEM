using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_DW_DimWorkOrder
    {
        [StringLength(50)]
        public string DataSource { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public long DimWorkOrderKey { get; set; }
        public short DimWorkOrderStatusKey { get; set; }
        public long? DimHeatKey { get; set; }
        public long? DimSteelgradeKey { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        public long DimProductCatalogueKey { get; set; }
        public long? DimCustomerKey { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public bool IsTestOrder { get; set; }
        public double TargetOrderWeight { get; set; }
        public DateTime? WorkOrderCreated { get; set; }
        public DateTime? WorkOrderLastUpdate { get; set; }
        public DateTime? CreatedInL3 { get; set; }
        public DateTime? ToBeCompletedBefore { get; set; }
    }
}
