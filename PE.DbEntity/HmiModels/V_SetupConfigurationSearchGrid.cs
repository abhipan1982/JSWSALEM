using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupConfigurationSearchGrid
    {
        public long ConfigurationId { get; set; }
        [Required]
        [StringLength(50)]
        public string ConfigurationName { get; set; }
        public short ConfigurationVersion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ConfigurationCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfigurationLastSentTs { get; set; }
    }
}
