using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class ZPCZebraPrinter
    {
        [Key]
        public long ZebraPrinterId { get; set; }
        [Required]
        [StringLength(50)]
        public string ZebraPrinterName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ZebraPrinterHostname { get; set; }
        public int? ZebraPrinterPort { get; set; }
        public long? FKAssetId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.ZPCZebraPrinters))]
        public virtual MVHAsset FKAsset { get; set; }
    }
}
