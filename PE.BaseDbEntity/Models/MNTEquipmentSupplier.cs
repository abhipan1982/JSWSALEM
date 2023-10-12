using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class MNTEquipmentSupplier
    {
        public MNTEquipmentSupplier()
        {
            MNTEquipments = new HashSet<MNTEquipment>();
        }

        [Key]
        public long EquipmentSupplierId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Required]
        [StringLength(50)]
        public string EquipmentSupplierName { get; set; }
        [StringLength(200)]
        public string EquipmentSupplierAddress { get; set; }
        [StringLength(150)]
        public string EquipmentSupplierEmail { get; set; }
        [StringLength(20)]
        public string EquipmentSupplierPhone { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(MNTEquipment.FKEquipmentSupplier))]
        public virtual ICollection<MNTEquipment> MNTEquipments { get; set; }
    }
}
