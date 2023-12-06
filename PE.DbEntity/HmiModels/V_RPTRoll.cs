using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PE.DbEntity.HmiModels
{
  [Keyless]
  public class V_RPTRoll
  {
    [Required] [StringLength(50)] public string RollTypeName { get; set; }

    public long RollId { get; set; }

    [Required] [StringLength(50)] public string RollName { get; set; }

    public short RollStatus { get; set; }

    [StringLength(50)] public string RollStatusName { get; set; }

    public double InitialDiameter { get; set; }
    public double ActualDiameter { get; set; }

    [StringLength(50)] public string RollSetName { get; set; }

    public short? RollSetStatus { get; set; }

    [StringLength(50)] public string RollSetStatusName { get; set; }

    [Column(TypeName = "datetime")] public DateTime? RollSetCreated { get; set; }

    [Column(TypeName = "datetime")] public DateTime? RollSetMounted { get; set; }

    [Column(TypeName = "datetime")] public DateTime? RollSetDismounted { get; set; }

    [StringLength(50)] public string CassetteName { get; set; }

    public short? CassetteStatus { get; set; }

    [StringLength(50)] public string CassetteStatusName { get; set; }

    [Required] [StringLength(50)] public string StandName { get; set; }

    public short? StandNo { get; set; }

    [StringLength(30)] public string StandZoneName { get; set; }

    public short? StandStatus { get; set; }

    [StringLength(50)] public string StandStatusName { get; set; }

    public int? RollsInRollSet { get; set; }
    public long? AccBilletCnt { get; set; }
    public double? AccWeight { get; set; }
  }
}
