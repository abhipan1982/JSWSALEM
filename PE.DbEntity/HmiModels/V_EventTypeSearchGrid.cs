using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_EventTypeSearchGrid
    {
        public long EventTypeId { get; set; }
        public short EventTypeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EventTypeName { get; set; }
        public long? ParentEventTypeId { get; set; }
        public short? ParentEventTypeCode { get; set; }
        [StringLength(50)]
        public string ParentEventTypeName { get; set; }
    }
}
