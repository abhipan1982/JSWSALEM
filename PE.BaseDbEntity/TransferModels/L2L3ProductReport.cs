using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Table("L2L3ProductReport", Schema = "xfr")]
    public partial class L2L3ProductReport
    {
        [Key]
        public long Counter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedTs { get; set; }
        public bool IsUpdated { get; set; }
        public short CommStatus { get; set; }
        [StringLength(400)]
        public string CommMessage { get; set; }
        [StringLength(4000)]
        public string ValidationCheck { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftName { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(50)]
        public string SteelgradeCode { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(5)]
        public string SequenceInWorkOrder { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(1)]
        public string ProductType { get; set; }
        [Required]
        [StringLength(10)]
        public string OutputWeight { get; set; }
        [Required]
        [StringLength(10)]
        public string OutputWidth { get; set; }
        [Required]
        [StringLength(10)]
        public string OutputThickness { get; set; }
        [Required]
        [StringLength(3)]
        public string OutputPieces { get; set; }
        [Required]
        [StringLength(1)]
        public string InspectionResult { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
    }
}
