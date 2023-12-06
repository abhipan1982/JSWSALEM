using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_DW_DimReheatingGroup
  {
    [StringLength(50)] public string DataSource { get; set; }

    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public long DimReheatingGroupKey { get; set; }

    [Required] [StringLength(10)] public string ReheatingGroupCode { get; set; }

    [Required] [StringLength(50)] public string ReheatingGroupName { get; set; }
  }
}
