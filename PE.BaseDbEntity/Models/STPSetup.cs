using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKSetupTypeId), Name = "NCI_SetupType")]
    [Index(nameof(SetupName), nameof(FKSetupTypeId), Name = "UQ_SetupName", IsUnique = true)]
    public partial class STPSetup
    {
        public STPSetup()
        {
            STPSetupConfigurations = new HashSet<STPSetupConfiguration>();
            STPSetupInstructions = new HashSet<STPSetupInstruction>();
            STPSetupParameters = new HashSet<STPSetupParameter>();
            STPSetupSents = new HashSet<STPSetupSent>();
            STPSetupWorkOrders = new HashSet<STPSetupWorkOrder>();
        }

        [Key]
        public long SetupId { get; set; }
        public long FKSetupTypeId { get; set; }
        [StringLength(10)]
        public string SetupCode { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupName { get; set; }
        [StringLength(100)]
        public string SetupDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(FKSetupTypeId))]
        [InverseProperty(nameof(STPSetupType.STPSetups))]
        public virtual STPSetupType FKSetupType { get; set; }
        [InverseProperty(nameof(STPSetupConfiguration.FKSetup))]
        public virtual ICollection<STPSetupConfiguration> STPSetupConfigurations { get; set; }
        [InverseProperty(nameof(STPSetupInstruction.FKSetup))]
        public virtual ICollection<STPSetupInstruction> STPSetupInstructions { get; set; }
        [InverseProperty(nameof(STPSetupParameter.FKSetup))]
        public virtual ICollection<STPSetupParameter> STPSetupParameters { get; set; }
        [InverseProperty(nameof(STPSetupSent.FKSetup))]
        public virtual ICollection<STPSetupSent> STPSetupSents { get; set; }
        [InverseProperty(nameof(STPSetupWorkOrder.FKSetup))]
        public virtual ICollection<STPSetupWorkOrder> STPSetupWorkOrders { get; set; }
    }
}
