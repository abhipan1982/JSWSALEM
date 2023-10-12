using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Table("MNTEquipmentHistory")]
    public partial class MNTEquipmentHistory
    {
        [Key]
        public long EquipmentHistoryId { get; set; }
        public long FKEquipmentId { get; set; }
        public long? MaterialsProcessed { get; set; }
        public double? WeightProcessed { get; set; }
        public short EquipmentStatus { get; set; }
        [StringLength(200)]
        public string Remark { get; set; }
        public ServiceType EnumServiceType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKEquipmentId))]
        [InverseProperty(nameof(MNTEquipment.MNTEquipmentHistories))]
        public virtual MNTEquipment FKEquipment { get; set; }
    }
}
