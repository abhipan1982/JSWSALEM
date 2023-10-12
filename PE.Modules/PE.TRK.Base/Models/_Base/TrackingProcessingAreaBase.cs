using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;

namespace PE.TRK.Base.Models._Base
{
  public abstract class TrackingProcessingAreaBase : TrackingSimpleAreaBase
  {
    public List<TrackingChildInstructionMeta> ChildInstructionsMeta { get; set; } = new List<TrackingChildInstructionMeta>();
    
    public TrackingProcessingAreaBase(int assetCode)
      : base(assetCode)
    {
    }

    public abstract ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request);

    public virtual void AddChildInstructionMeta(TrackingChildInstructionMeta request)
    {
      ChildInstructionsMeta.Add(request);
    }

    public virtual bool IsChildInstructionProcessedInTimeFilter(long instructionId, DateTime minOperationDate)
    {
      return ChildInstructionsMeta.Any(x => x.InstructionId == instructionId && x.OperationDate >= minOperationDate);
    }

    public virtual void RemoveAllChildInstructionsMetaByInstructionId(long instructionId)
    {
      ChildInstructionsMeta.RemoveAll(x => x.InstructionId == instructionId);
    }
  }

  public class TrackingChildInstructionMeta
  {
    public long InstructionId { get; set; }
    public DateTime OperationDate { get; set; }
  }
}
