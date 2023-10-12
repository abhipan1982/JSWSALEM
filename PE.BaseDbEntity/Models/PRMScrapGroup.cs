using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class PRMScrapGroup
    {
        public PRMScrapGroup()
        {
            PRMSteelgrades = new HashSet<PRMSteelgrade>();
        }

        [Key]
        public long ScrapGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string ScrapGroupCode { get; set; }
        [StringLength(50)]
        public string ScrapGroupName { get; set; }
        [StringLength(200)]
        public string ScrapGroupDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(PRMSteelgrade.FKScrapGroup))]
        public virtual ICollection<PRMSteelgrade> PRMSteelgrades { get; set; }
    }
}
