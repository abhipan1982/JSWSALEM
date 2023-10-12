using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(HeatName), Name = "UQ_UniqueHeatName", IsUnique = true)]
    public partial class PRMHeat
    {
        public PRMHeat()
        {
            PRMHeatChemicalAnalyses = new HashSet<PRMHeatChemicalAnalysis>();
            PRMMaterials = new HashSet<PRMMaterial>();
            PRMWorkOrders = new HashSet<PRMWorkOrder>();
        }

        [Key]
        public long HeatId { get; set; }
        public long? FKSteelgradeId { get; set; }
        public long? FKHeatSupplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        public double? HeatWeight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeatCreatedTs { get; set; }
        public bool IsDummy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKHeatSupplierId))]
        [InverseProperty(nameof(PRMHeatSupplier.PRMHeats))]
        public virtual PRMHeatSupplier FKHeatSupplier { get; set; }
        [ForeignKey(nameof(FKSteelgradeId))]
        [InverseProperty(nameof(PRMSteelgrade.PRMHeats))]
        public virtual PRMSteelgrade FKSteelgrade { get; set; }
        [InverseProperty(nameof(PRMHeatChemicalAnalysis.FKHeat))]
        public virtual ICollection<PRMHeatChemicalAnalysis> PRMHeatChemicalAnalyses { get; set; }
        [InverseProperty(nameof(PRMMaterial.FKHeat))]
        public virtual ICollection<PRMMaterial> PRMMaterials { get; set; }
        [InverseProperty(nameof(PRMWorkOrder.FKHeat))]
        public virtual ICollection<PRMWorkOrder> PRMWorkOrders { get; set; }
    }
}
