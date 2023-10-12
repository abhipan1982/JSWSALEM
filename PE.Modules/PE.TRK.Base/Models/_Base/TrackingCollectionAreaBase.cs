using System;
using System.Collections.Generic;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models._Base
{
  public abstract class TrackingCollectionAreaBase : TrackingProcessingAreaBase, ITrackingSimpleCollectionAreaBase
  {
    protected readonly object LockObject = new object();
    protected readonly ITrackingEventStorageProviderBase TrackingEventStorageProvider;

    public bool IsPositionBasedCollection { get; protected set; }
    public int PositionsAmount { get; protected set; }
    public int VirtualPositionsAmount { get; protected set; }

    public TrackingCollectionAreaBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      bool isPositionBasedCollection,
      int positionsAmount,
      int virtualPositionsAmount)
      : base(areaAssetCode)
    {
      IsPositionBasedCollection = isPositionBasedCollection;
      PositionsAmount = positionsAmount;
      VirtualPositionsAmount = virtualPositionsAmount;
      TrackingEventStorageProvider = trackingEventStorageProvider;
    }

    public abstract void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false);
    public abstract void Drop(long rawMaterialId, int dropSeq, int? dragSeq = null);

    protected abstract void QueuePositionChange();

    /// <summary>
    /// If assetCode = null => AreaAssetCode will be used
    /// </summary>
    /// <param name="materialId"></param>
    /// <param name="eventType"></param>
    /// <param name="triggerDate"></param>
    /// <param name="isArea"></param>
    /// <param name="assetCode"></param>
    protected abstract void TriggerTrackingEvent(long materialId,
      TrackingEventType eventType,
      DateTime triggerDate,
      bool isArea = true,
      int? assetCode = null);

    public abstract List<MaterialPosition> GetMaterials();
    public abstract ITrackingInstructionDataContractBase ChargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false);
    public abstract ITrackingInstructionDataContractBase DischargeElement(DateTime operationDate);
    public abstract void RemoveLastVirtualPosition();
    public abstract ITrackingInstructionDataContractBase GetFirstVirtualPosition();
    public abstract ITrackingInstructionDataContractBase GetLastVirtualPosition();
    public abstract List<long> GetMaterialIds();
    public abstract void RemoveMaterialFromCollection(long materialId, DateTime operationDate);

    public override ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request)
    {
      var result = new TrackingInstructionDataContractBase();
      switch (request.InstructionType)
      {
        case var instructionType when instructionType == TrackingInstructionType.Charge:
          return ProcessChargeInstruction(request);
        case var instructionType when instructionType == TrackingInstructionType.Discharge:
          return ProcessDischargeInstruction(request);
        case var instructionType when instructionType == TrackingInstructionType.RemoveVirtualPosition:
          ProcessRemoveVirtualPosition();
          return result;
        case var instructionType when instructionType == TrackingInstructionType.GetVirtualPosition:
          return GetLastVirtualPosition();
        default:
          throw new ArgumentOutOfRangeException($"Instruction Type {request.InstructionType} is not implemented for {AreaAssetCode}");
      }
    }

    private ITrackingInstructionDataContractBase ProcessChargeInstruction(ITrackingInstructionRequest request)
    {
      if (request.PreviousOperationResult == null)
        throw new ArgumentException($"For instruction type {request.InstructionType} PreviousOperationResult should not be null. Area: {AreaAssetCode}");

      return ChargeElement(request.PreviousOperationResult, request.OperationDate);
    }

    private void ProcessRemoveVirtualPosition()
    {
      RemoveLastVirtualPosition();
    }

    private ITrackingInstructionDataContractBase ProcessDischargeInstruction(ITrackingInstructionRequest request)
    {
      return DischargeElement(request.OperationDate);
    }
  }
}
