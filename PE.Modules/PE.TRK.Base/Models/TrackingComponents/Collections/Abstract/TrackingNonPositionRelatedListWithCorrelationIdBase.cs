using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.Helpers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public abstract class TrackingNonPositionRelatedListWithCorrelationIdBase
    : TrackingNonPositionRelatedListAbstractBase
  {
    public DateTime? EnterSignalTs { get; private set; }
    public DateTime? CorrelationIdTs { get; private set; }
    public TrackingCorrelation ActiveCorrelationId { get; private set; }

    public TrackingNonPositionRelatedListWithCorrelationIdBase(
      ITrackingEventStorageProviderBase trackingEventStorageProvider, 
      int areaAssetCode, 
      List<QueuePosition> positionsToBeInitialized, 
      int positionsAmount, 
      int virtualPositionsAmount) 
      : base(trackingEventStorageProvider, 
          areaAssetCode, 
          positionsToBeInitialized, 
          positionsAmount, 
          virtualPositionsAmount)
    {
    }

    public virtual void SetEnterSignalTs(DateTime enterSignalTs)
    {
      EnterSignalTs = enterSignalTs;
    }

    public virtual void SetCorrelationId(TrackingCorrelation correlationId, DateTime correlationIdTs)
    {
      CorrelationIdTs = correlationIdTs;
      ActiveCorrelationId = correlationId;
    }

    public virtual TrackingCollectionElementAbstractBase DischargeByCorrelationId(DateTime operationDate, TrackingCorrelation correlationId)
    {
      var elementToDischarge = Elements.FirstOrDefault(x => x.MaterialInfoCollection.MaterialInfos.Any(x => x.CorrelationId.Equals(correlationId)));

      if(elementToDischarge != null)
      {
        if (VirtualPositionsAmount > 0)
          VirtualElements.Add(elementToDischarge);

        var materialInfo = elementToDischarge.MaterialInfoCollection.MaterialInfos.FirstOrDefault();

        materialInfo?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        Elements.Remove(elementToDischarge);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(materialInfo?.MaterialId ?? 0,
          TrackingEventType.Exit,
          operationDate));
      }

      return elementToDischarge;
    }

    public override ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request)
    {
      switch (request.InstructionType)
      {
        case var instructionType when instructionType == TrackingInstructionType.SetEnterTs:
          return ProcessSetEnterTsInstruction(request);
        case var instructionType when instructionType == TrackingInstructionType.SetCorrelationId:
          return ProcessCorrelationIdInstruction(request);
        default:
          return base.ProcessInstruction(request);
      }
    }

    protected virtual ITrackingInstructionDataContractBase ProcessCorrelationIdInstruction(ITrackingInstructionRequest request)
    {
      if(request is not TrackingInstructionWithCorrelationIdRequest instruction)
      {
        throw new ArgumentException($"For processing instruction {nameof(ProcessCorrelationIdInstruction)} request should be of type : {nameof(TrackingInstructionWithCorrelationIdRequest)}");
      }

      SetCorrelationId(instruction.CorrelationId, instruction.OperationDate);

      return request.PreviousOperationResult;
    }

    protected virtual ITrackingInstructionDataContractBase ProcessSetEnterTsInstruction(ITrackingInstructionRequest request)
    {
      SetEnterSignalTs(request.OperationDate);

      return request.PreviousOperationResult;
    }
  }
}
