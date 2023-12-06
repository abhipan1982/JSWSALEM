using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_SG_RawMaterial
  {
    public long RawMaterialId { get; set; }

    [Required] [StringLength(50)] public string MaterialName { get; set; }

    [Required] [StringLength(50)] public string RawMaterialName { get; set; }

    [StringLength(50)] public string L3MaterialName { get; set; }

    [Column(TypeName = "datetime")] public DateTime AUDCreatedTs { get; set; }

    [Column(TypeName = "datetime")] public DateTime AUDLastUpdatedTs { get; set; }

    public short RawMaterialStatus { get; set; }
  }
}
