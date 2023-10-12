using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.BaseDbEntity.TransferModels
{
    [Table("L2L3WorkOrderReport", Schema = "xfr")]
    public partial class L2L3WorkOrderReport
    {
        [Key]
        public long Counter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AUDCreatedTs { get; set; }
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
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [Required]
        [StringLength(1)]
        public string IsWorkOrderFinished { get; set; }
        [Required]
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string InputWidth { get; set; }
        [Required]
        [StringLength(10)]
        public string InputThickness { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [Required]
        [StringLength(10)]
        public string OutputWidth { get; set; }
        [Required]
        [StringLength(10)]
        public string OutputThickness { get; set; }
        [Required]
        [StringLength(50)]
        public string HeatName { get; set; }
        [Required]
        [StringLength(50)]
        public string SteelgradeCode { get; set; }
        [Required]
        [StringLength(10)]
        public string TargetWorkOrderWeight { get; set; }
        [Required]
        [StringLength(10)]
        public string TotalWeightOfMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string DelayDuration { get; set; }
        [Required]
        [StringLength(10)]
        public string TotalWeightOfProducts { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfPlannedMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfChargedMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfScrappedMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfRejectedMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfRolledMaterials { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfProducts { get; set; }
        [Required]
        [StringLength(10)]
        public string NumberOfPiecesRejectedInLocation1 { get; set; }
        [Required]
        [StringLength(10)]
        public string WeightOfPiecesRejectedInLocation1 { get; set; }
        [Required]
        [StringLength(14)]
        public string WorkOrderStart { get; set; }
        [Required]
        [StringLength(14)]
        public string WorkOrderEnd { get; set; }
        [Required]
        [StringLength(10)]
        public string ShiftName { get; set; }
        [Required]
        [StringLength(50)]
        public string CrewName { get; set; }
    }
}
