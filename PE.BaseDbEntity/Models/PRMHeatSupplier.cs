using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class PRMHeatSupplier
    {
        public PRMHeatSupplier()
        {
            PRMHeats = new HashSet<PRMHeat>();
        }

        [Key]
        public long HeatSupplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatSupplierName { get; set; }
        [StringLength(200)]
        public string HeatSupplierDescription { get; set; }
        public bool IsDefault { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMHeat.FKHeatSupplier))]
        public virtual ICollection<PRMHeat> PRMHeats { get; set; }
    }
}
