using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_RPTConsumption
  {
    [StringLength(4000)] public string DimYear { get; set; }

    [StringLength(4000)] public string DimMonth { get; set; }

    [Required] [StringLength(4000)] public string DimWeek { get; set; }

    [StringLength(4000)] public string DimDate { get; set; }

    public double? GAS { get; set; }
    public double? WATER { get; set; }
    public double? ELECTRICITY { get; set; }
  }
}
