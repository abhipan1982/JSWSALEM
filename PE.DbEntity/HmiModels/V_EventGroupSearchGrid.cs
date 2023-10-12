using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_EventGroupSearchGrid
    {
        public long EventCategoryGroupId { get; set; }
        [Required]
        [StringLength(10)]
        public string EventCategoryGroupCode { get; set; }
        [StringLength(50)]
        public string EventCategoryGroupName { get; set; }
    }
}
