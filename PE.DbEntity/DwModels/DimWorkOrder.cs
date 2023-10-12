using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimWorkOrder
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimWorkOrderRow { get; set; }
        public bool DimWorkOrderIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimWorkOrderHash { get; set; }
        public long DimWorkOrderKey { get; set; }
        public short DimWorkOrderStatusKey { get; set; }
        public long? DimHeatKey { get; set; }
        public long DimSteelgradeKey { get; set; }
        public long DimMaterialCatalogueKey { get; set; }
        public long DimProductCatalogueKey { get; set; }
        public long? DimCustomerKey { get; set; }
        public long? DimWorkOrderKeyParent { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public bool WorkOrderIsTest { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderCreatedInL3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WorkOrderDueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkOrderEnd { get; set; }
        [StringLength(50)]
        public string WoorkOrderExternalName { get; set; }
        [StringLength(50)]
        public string WorkOrderNameParent { get; set; }
    }
}
