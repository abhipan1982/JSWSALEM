using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTEquipment
    {
        public MNTEquipment()
        {
            InverseFKParentEquipment = new HashSet<MNTEquipment>();
            MNTEquipmentHistories = new HashSet<MNTEquipmentHistory>();
            MNTMaintenanceSchedules = new HashSet<MNTMaintenanceSchedule>();
        }

        [Key]
        public long EquipmentId { get; set; }
        [Required]
        [StringLength(10)]
        public string EquipmentCode { get; set; }
        [StringLength(50)]
        public string EquipmentName { get; set; }
        [StringLength(100)]
        public string EquipmentDescription { get; set; }
        public long FKEquipmentGroupId { get; set; }
        public long? FKEquipmentSupplierId { get; set; }
        public long? FKAssetId { get; set; }
        public long? FKParentEquipmentId { get; set; }
        public short EquipmentStatus { get; set; }
        public short AccumulationType { get; set; }
        public double? ActualValue { get; set; }
        public double? WarningValue { get; set; }
        public double? AlarmValue { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ServiceExpires { get; set; }
        public long? CntMatsProcessed { get; set; }
        public ServiceType EnumServiceType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKAssetId))]
        [InverseProperty(nameof(MVHAsset.MNTEquipments))]
        public virtual MVHAsset FKAsset { get; set; }
        [ForeignKey(nameof(FKEquipmentGroupId))]
        [InverseProperty(nameof(MNTEquipmentGroup.MNTEquipments))]
        public virtual MNTEquipmentGroup FKEquipmentGroup { get; set; }
        [ForeignKey(nameof(FKEquipmentSupplierId))]
        [InverseProperty(nameof(MNTEquipmentSupplier.MNTEquipments))]
        public virtual MNTEquipmentSupplier FKEquipmentSupplier { get; set; }
        [ForeignKey(nameof(FKParentEquipmentId))]
        [InverseProperty(nameof(MNTEquipment.InverseFKParentEquipment))]
        public virtual MNTEquipment FKParentEquipment { get; set; }
        [InverseProperty(nameof(MNTEquipment.FKParentEquipment))]
        public virtual ICollection<MNTEquipment> InverseFKParentEquipment { get; set; }
        [InverseProperty(nameof(MNTEquipmentHistory.FKEquipment))]
        public virtual ICollection<MNTEquipmentHistory> MNTEquipmentHistories { get; set; }
        [InverseProperty(nameof(MNTMaintenanceSchedule.FKEquipment))]
        public virtual ICollection<MNTMaintenanceSchedule> MNTMaintenanceSchedules { get; set; }
    }
}
