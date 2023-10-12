using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKParentSteelgradeId), Name = "NCI_ParentSteelgradeId")]
    [Index(nameof(FKScrapGroupId), Name = "NCI_ScrapGroupId")]
    [Index(nameof(FKSteelGroupId), Name = "NCI_SteelGroupId")]
    [Index(nameof(SteelgradeCode), Name = "UQ_SteelgradeCode", IsUnique = true)]
    [Index(nameof(SteelgradeName), Name = "UQ_SteelgradeName", IsUnique = true)]
    public partial class PRMSteelgrade
    {
        public PRMSteelgrade()
        {
            InverseFKParentSteelgrade = new HashSet<PRMSteelgrade>();
            PRMHeats = new HashSet<PRMHeat>();
            PRMWorkOrders = new HashSet<PRMWorkOrder>();
        }

        [Key]
        public long SteelgradeId { get; set; }
        public long? FKSteelGroupId { get; set; }
        public long? FKScrapGroupId { get; set; }
        public long? FKParentSteelgradeId { get; set; }
        public bool IsDefault { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
        [StringLength(200)]
        public string SteelgradeDescription { get; set; }
        [StringLength(10)]
        public string CustomCode { get; set; }
        [StringLength(50)]
        public string CustomName { get; set; }
        [StringLength(200)]
        public string CustomDescription { get; set; }
        public double? Density { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKParentSteelgradeId))]
        [InverseProperty(nameof(PRMSteelgrade.InverseFKParentSteelgrade))]
        public virtual PRMSteelgrade FKParentSteelgrade { get; set; }
        [ForeignKey(nameof(FKScrapGroupId))]
        [InverseProperty(nameof(PRMScrapGroup.PRMSteelgrades))]
        public virtual PRMScrapGroup FKScrapGroup { get; set; }
        [ForeignKey(nameof(FKSteelGroupId))]
        [InverseProperty(nameof(PRMSteelGroup.PRMSteelgrades))]
        public virtual PRMSteelGroup FKSteelGroup { get; set; }
        [InverseProperty("FKSteelgrade")]
        public virtual PRMSteelgradeChemicalComposition PRMSteelgradeChemicalComposition { get; set; }
        [InverseProperty(nameof(PRMSteelgrade.FKParentSteelgrade))]
        public virtual ICollection<PRMSteelgrade> InverseFKParentSteelgrade { get; set; }
        [InverseProperty(nameof(PRMHeat.FKSteelgrade))]
        public virtual ICollection<PRMHeat> PRMHeats { get; set; }
        [InverseProperty(nameof(PRMWorkOrder.FKSteelgrade))]
        public virtual ICollection<PRMWorkOrder> PRMWorkOrders { get; set; }
    }
}
