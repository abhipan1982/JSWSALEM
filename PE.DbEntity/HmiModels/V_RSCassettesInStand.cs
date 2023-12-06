using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_RSCassettesInStand
  {
    public long StandId { get; set; }
    public short Status { get; set; }
    public short StandNo { get; set; }

    [StringLength(30)] public string StandZoneName { get; set; }

    public bool IsOnLine { get; set; }
    public short? Position { get; set; }
    public long? CassetteId { get; set; }
    public short? CassetteStatus { get; set; }

    [StringLength(50)] public string CassetteName { get; set; }

    public short? Arrangement { get; set; }

    [StringLength(50)] public string CassetteTypeName { get; set; }

    public short? Type { get; set; }
    public long? CassetteTypeId { get; set; }
  }
}
