using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.DWModels
{
    [Keyless]
    public partial class DimShiftDefinition
    {
        [Required]
        [StringLength(50)]
        public string SourceName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceTime { get; set; }
        public long DimShiftDefinitionRow { get; set; }
        public bool DimShiftDefinitionIsDeleted { get; set; }
        [MaxLength(16)]
        public byte[] DimShiftDefinitionHash { get; set; }
        public long DimShiftDefinitionKey { get; set; }
        public long? DimShiftDefinitionKeyNext { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftCode { get; set; }
        public TimeSpan ShiftDefaultStartTime { get; set; }
        public TimeSpan ShiftDefaultEndTime { get; set; }
        public bool ShiftEndsNextDay { get; set; }
    }
}
