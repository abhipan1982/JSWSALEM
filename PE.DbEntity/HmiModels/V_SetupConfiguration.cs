using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupConfiguration
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
        public long SetupTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsSteelgradeRelated { get; set; }
        public long SetupId { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SetupCreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SetupUpdatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SetupConfigurationLastSentTs { get; set; }
        public long SetupConfigurationId { get; set; }
    }
}
