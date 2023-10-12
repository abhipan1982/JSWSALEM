using System;
using System.Collections.Generic;
using System.Text;
using PE.BaseDbEntity.EnumClasses;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class TrackingInstruction
  {
    public long InstructionId { get; set; }
    public int FeatureCode { get; set; }
    public short? TrackingInstructionValue { get; set; }
    public short SeqNo { get; set; }
    public TrackingInstructionType TrackingInstructionType {  get; set; }
    public int AreaAssetCode { get; set; }
    public int? PointAssetCode { get; set; }
    public bool IgnoreIfSimulation { get; set; }
    public long? ParentInstructionId { get; set; }
    public double? TimeFilter { get; set; }
    public bool IsProcessedDuringAdjustment { get; set; }

    public TrackingInstruction ParentInstruction { get; set; }
    public List<TrackingInstruction> ChildInstructions { get; set; } = new List<TrackingInstruction>();
  }
}
