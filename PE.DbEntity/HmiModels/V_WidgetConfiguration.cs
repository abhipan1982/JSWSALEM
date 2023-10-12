using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_WidgetConfiguration
    {
        public long? WidgetId { get; set; }
        [StringLength(50)]
        public string WidgetName { get; set; }
        [StringLength(250)]
        public string WidgetFileName { get; set; }
        public long? WidgetConfigurationId { get; set; }
        public short? OrderSeq { get; set; }
        public bool? IsActive { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
    }
}
