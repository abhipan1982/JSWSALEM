using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(FKEventId), Name = "NCI_EventId")]
    public partial class HMIRefreshKey
    {
        [Key]
        public long HmiRefreshKeyId { get; set; }
        public long FKEventId { get; set; }
        [Required]
        [Column("HmiRefreshKey")]
        [StringLength(150)]
        public string HmiRefreshKey1 { get; set; }
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
