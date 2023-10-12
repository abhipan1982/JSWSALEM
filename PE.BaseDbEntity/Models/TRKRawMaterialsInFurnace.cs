using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("TRKRawMaterialsInFurnace")]
    [Index(nameof(FKRawMaterialId), Name = "NCI_FKRawMaterialId")]
    [Index(nameof(OrderSeq), Name = "NCI_OrderSeq")]
    public partial class TRKRawMaterialsInFurnace
    {
        [Key]
        public long RawMaterialInFurnaceId { get; set; }
        public long? FKRawMaterialId { get; set; }
        public short OrderSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChargingTs { get; set; }
        public bool IsTimerMeasurement { get; set; }
        public int TimeInFurnace { get; set; }
        public double? Temperature { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKRawMaterialId))]
        [InverseProperty(nameof(TRKRawMaterial.TRKRawMaterialsInFurnaces))]
        public virtual TRKRawMaterial FKRawMaterial { get; set; }
    }
}
