using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class L1ABypassInstance
    {
        [Key]
        public long BypassInstanceId { get; set; }
        public long FKBypassConfigurationId { get; set; }
        [Required]
        [StringLength(1000)]
        public string OpcBypassNode { get; set; }

        [ForeignKey(nameof(FKBypassConfigurationId))]
        [InverseProperty(nameof(L1ABypassConfiguration.L1ABypassInstances))]
        public virtual L1ABypassConfiguration FKBypassConfiguration { get; set; }
    }
}
