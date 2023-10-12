using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(SteelGroupName), Name = "UQ_SteelGroupName", IsUnique = true)]
    public partial class PRMSteelGroup
    {
        public PRMSteelGroup()
        {
            PRMSteelgrades = new HashSet<PRMSteelgrade>();
        }

        [Key]
        public long SteelGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string SteelGroupCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SteelGroupName { get; set; }
        [StringLength(200)]
        public string SteelGroupDescription { get; set; }
        public bool IsDefault { get; set; }
        public double WearCoefficient { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMSteelgrade.FKSteelGroup))]
        public virtual ICollection<PRMSteelgrade> PRMSteelgrades { get; set; }
    }
}
