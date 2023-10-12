using PE.BaseDbEntity.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.Models
{
    [Index(nameof(MillControlCode), Name = "UQ_TRKMillControlCode", IsUnique = true)]
    public partial class TRKMillControlData
    {
        [Key]
        public long MillControlDataId { get; set; }
        public int MillControlCode { get; set; }
        public CommChannelType EnumCommChannelType { get; set; }
        [StringLength(350)]
        public string CommAttr1 { get; set; }
        [StringLength(350)]
        public string CommAttr2 { get; set; }
        [StringLength(350)]
        public string CommAttr3 { get; set; }
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
