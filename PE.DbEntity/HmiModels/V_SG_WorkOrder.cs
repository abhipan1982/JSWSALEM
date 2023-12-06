using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_SG_WorkOrder
  {
    public long WorkOrderId { get; set; }

    [Required] [StringLength(50)] public string WorkOrderName { get; set; }

    public short WorkOrderStatus { get; set; }

    [Column(TypeName = "datetime")] public DateTime OrderDeadline { get; set; }

    public double TargetOrderWeight { get; set; }

    [Column(TypeName = "datetime")] public DateTime? ProductionStart { get; set; }

    [Column(TypeName = "datetime")] public DateTime? ProductionEnd { get; set; }

    [Required] [StringLength(50)] public string Layout { get; set; }

    public short? TMPRO { get; set; }

    [Required] [StringLength(50)] public string ProductCatalogueName { get; set; }

    [Required] [StringLength(50)] public string MaterialCatalogueName { get; set; }

    [Required] [StringLength(10)] public string SteelgradeCode { get; set; }

    [StringLength(50)] public string SteelgradeName { get; set; }

    [Required] [StringLength(50)] public string HeatName { get; set; }
  }
}
