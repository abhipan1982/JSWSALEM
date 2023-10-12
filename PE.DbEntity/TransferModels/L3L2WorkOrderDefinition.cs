using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PE.DbEntity.TransferModels
{
    [Table("L3L2WorkOrderDefinition", Schema = "xfr")]
    [Index(nameof(CommStatus), nameof(UpdatedTs), Name = "NCI_CommStatus_UpdatedTs")]
    [Index(nameof(WorkOrderName), nameof(CounterId), Name = "NCI_WorkOrderName_CounterId")]
    public partial class L3L2WorkOrderDefinition
    {
        [Key]
        public long CounterId { get; set; }
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
        [StringLength(50)]
        public string WorkOrderName { get; set; }
        [StringLength(50)]
        public string ExternalWorkOrderName { get; set; }
        [StringLength(50)]
        public string PreviousWorkOrderName { get; set; }
        [StringLength(8)]
        public string OrderDeadline { get; set; }
        [StringLength(50)]
        public string HeatName { get; set; }
        [StringLength(3)]
        public string NumberOfBillets { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(10)]
        public string BundleWeightMin { get; set; }
        [StringLength(10)]
        public string BundleWeightMax { get; set; }
        [StringLength(10)]
        public string TargetWorkOrderWeight { get; set; }
        [StringLength(10)]
        public string TargetWorkOrderWeightMin { get; set; }
        [StringLength(10)]
        public string TargetWorkOrderWeightMax { get; set; }
        [StringLength(50)]
        public string MaterialCatalogueName { get; set; }
        [StringLength(50)]
        public string ProductCatalogueName { get; set; }
        [StringLength(50)]
        public string SteelgradeCode { get; set; }
        [StringLength(10)]
        public string InputThickness { get; set; }
        [StringLength(10)]
        public string InputWidth { get; set; }
        [StringLength(10)]
        public string InputShapeSymbol { get; set; }
        [StringLength(10)]
        public string BilletWeight { get; set; }
        [StringLength(10)]
        public string BilletLength { get; set; }
        [StringLength(10)]
        public string OutputThickness { get; set; }
        [StringLength(10)]
        public string OutputWidth { get; set; }
        [StringLength(10)]
        public string OutputShapeSymbol { get; set; }
    }
}
