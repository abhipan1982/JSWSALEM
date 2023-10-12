using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    public partial class L1ABypassType
    {
        public L1ABypassType()
        {
            L1ABypassConfigurations = new HashSet<L1ABypassConfiguration>();
        }

        [Key]
        public short BypassTypeId { get; set; }
        public short BypassTypeCode { get; set; }
        [Required]
        [StringLength(250)]
        public string BypassTypeName { get; set; }

        [InverseProperty(nameof(L1ABypassConfiguration.FKBypassType))]
        public virtual ICollection<L1ABypassConfiguration> L1ABypassConfigurations { get; set; }
    }
}
