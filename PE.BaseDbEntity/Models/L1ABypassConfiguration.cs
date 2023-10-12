using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class L1ABypassConfiguration
    {
        public L1ABypassConfiguration()
        {
            L1ABypassInstances = new HashSet<L1ABypassInstance>();
        }

        [Key]
        public long BypassConfigurationId { get; set; }
        public short FKBypassTypeId { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcServerAddress { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcServerName { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcBypassParentStructureNode { get; set; }
        [Required]
        [StringLength(250)]
        public string OpcBypassName { get; set; }

        [ForeignKey(nameof(FKBypassTypeId))]
        [InverseProperty(nameof(L1ABypassType.L1ABypassConfigurations))]
        public virtual L1ABypassType FKBypassType { get; set; }
        [InverseProperty(nameof(L1ABypassInstance.FKBypassConfiguration))]
        public virtual ICollection<L1ABypassInstance> L1ABypassInstances { get; set; }
    }
}
