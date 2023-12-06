using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
    [Keyless]
    public partial class V_SetupWorkOrder
    {
        public long WorkOrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        public long SetupTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string SetupTypeName { get; set; }
        public bool IsRequired { get; set; }
        public long? SetupId { get; set; }
        [StringLength(50)]
        public string SetupName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CalculatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SentTs { get; set; }
        public bool? IsAmbiguous { get; set; }
        public double? ProductThickness { get; set; }
        [StringLength(10)]
        public string SteelgradeCode { get; set; }
        [StringLength(50)]
        public string SteelgradeName { get; set; }
    }
}
