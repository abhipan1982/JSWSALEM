using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKUserId), Name = "NCI_UserId")]
    public partial class HMIUserConfiguration
    {
        [Key]
        public long ConfigurationId { get; set; }
        public string FKUserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string ConfigurationName { get; set; }
        [StringLength(4000)]
        public string ConfigurationContent { get; set; }
        [StringLength(50)]
        public string ConfigurationType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDLastUpdatedTs { get; set; }
        [StringLength(255)]
        public string AUDUpdatedBy { get; set; }
        public bool IsBeforeCommit { get; set; }
        public bool IsDeleted { get; set; }
    }
}
