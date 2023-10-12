using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimWorkOrderStatus
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimWorkOrderStatusRow { get; set; }
        public bool DimWorkOrderStatusIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimWorkOrderStatusHash { get; set; }
        public long DimWorkOrderStatusKey { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string WorkOrderStatus { get; set; }
    }
}
