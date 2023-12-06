using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_Limit
    {
        public long LimitId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateTs { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public double? UpperValueFloat { get; set; }
        public double? LowerValueFloat { get; set; }
        public long? UnitId { get; set; }
        public int? UpperValueInt { get; set; }
        public int? LowerValueInt { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpperValueDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LowerValueDate { get; set; }
        public int ValueType { get; set; }
        [StringLength(50)]
        public string ValueTypeName { get; set; }
        public long LimitGroupId { get; set; }
        [Required]
        [StringLength(50)]
        public string LimitGroupName { get; set; }
        [StringLength(200)]
        public string LimitGroupDescription { get; set; }
    }
}
