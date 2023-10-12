using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTEquipmentGroup
    {
        public MNTEquipmentGroup()
        {
            InverseFKParentEquipmentGroup = new HashSet<MNTEquipmentGroup>();
            MNTEquipments = new HashSet<MNTEquipment>();
        }

        [Key]
        public long EquipmentGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string EquipmentGroupCode { get; set; }
        [StringLength(50)]
        public string EquipmentGroupName { get; set; }
        [StringLength(100)]
        public string EquipmentGroupDescription { get; set; }
        public long? FKParentEquipmentGroupId { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKParentEquipmentGroupId))]
        [InverseProperty(nameof(MNTEquipmentGroup.InverseFKParentEquipmentGroup))]
        public virtual MNTEquipmentGroup FKParentEquipmentGroup { get; set; }
        [InverseProperty(nameof(MNTEquipmentGroup.FKParentEquipmentGroup))]
        public virtual ICollection<MNTEquipmentGroup> InverseFKParentEquipmentGroup { get; set; }
        [InverseProperty(nameof(MNTEquipment.FKEquipmentGroup))]
        public virtual ICollection<MNTEquipment> MNTEquipments { get; set; }
    }
}
