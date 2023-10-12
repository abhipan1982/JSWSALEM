using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Helpers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionPositions.Concrete;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Abstract;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public class TrackingPositionRelatedCollectionAbstractBase : TrackingCollectionAreaBase, ICollectionMoveable
  {
    protected readonly Dictionary<long, short> LayerWithMaterialsCounterMap = new();
    protected List<TrackingCollectionPositionBase> Positions { get; } = new List<TrackingCollectionPositionBase>();

    public TrackingPositionRelatedCollectionAbstractBase(
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      int positionsAmount,
      int virtualPositionsAmount,
      List<QueuePosition> positionsToBeInitialized,
      Dictionary<long, short> layerWithMaterialsCounterMap = null)
    : base(trackingEventStorageProvider, areaAssetCode, true, positionsAmount, virtualPositionsAmount)
    {
      LayerWithMaterialsCounterMap = layerWithMaterialsCounterMap;
      InitPositions(positionsToBeInitialized);
    }

    #region public methods

    public override void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false)
    {
      lock (LockObject)
      {
        if (Positions.Count > dragSeq)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} is OutOfRange. Amount of elements: {Positions.Count}");

        var position = Positions.First(x => x.PositionId == dragSeq);

        if (position.Element == null || position.Element.MaterialInfoCollection.MaterialInfos.Count == 0)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} nothing to drag");

        position.Element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

        if (position.Element.MaterialInfoCollection.MaterialInfos.Count == 0)
          position.SetElement(null);

        TaskHelper.FireAndForget(QueuePositionChange);

        if (!isPreviousDragForTheSameAsset)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, TrackingEventType.Exit, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }
    public override void Drop(long rawMaterialId, int dropSeq, int? dragSeq = null)
    {
      lock (LockObject)
      {
        var positionsToCheck = Positions;

        if (dragSeq.HasValue && dragSeq <= Positions.Count)
        {
          var positionToExcludeCheck = Positions.First(x => x.PositionId == dragSeq.Value);
          positionsToCheck = Positions.Where(x => x != positionToExcludeCheck).ToList();
        }

        if (dropSeq > Positions.Count)
          throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dropSeq} is OutOfRange. Amount of elements: {Positions.Count}");

        var position = Positions.First(x => x.PositionId == dropSeq);

        if (positionsToCheck.Where(x => x != position).Any(x => x.Element?.MaterialInfoCollection[rawMaterialId] != null))
        {
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");
        }

        if (position.Element != null && position.Element.MaterialInfoCollection[rawMaterialId] != null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");


        if (position.Element != null)
          position.Element.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterialId));
        else
        {
          position.SetElement(new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));
        }

        TaskHelper.FireAndForget(QueuePositionChange);

        if (dragSeq != dropSeq)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, TrackingEventType.Enter, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override List<MaterialPosition> GetMaterials()
    {
      return (
        from position in Positions
        where position.Element != null
        let material = position.Element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()
        select new MaterialPosition()
        {
          RawMaterialId = material.MaterialId,
          Order = 1,
          PositionOrder = position.PositionId
        })
      .ToList();
    }

    public override List<long> GetMaterialIds()
    {
      return Positions
        .Where(p => p.Element != null)
        .SelectMany(p => p.Element.MaterialInfoCollection.MaterialInfos
          .Select(x => x.MaterialId))
        .ToList();
    }

    public override ITrackingInstructionDataContractBase ChargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false)
    {
      lock (LockObject)
      {
        if (Positions[0].Element != null)
        {
          throw new Exception("For ChargeElement operation first element should be empty");
        }

        var element = elementAbstract as TrackingCollectionElementAbstractBase;

        Positions[0].SetElement(element);

        if (element.MaterialInfoCollection != null)
          element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Charge);

        if (!ignoreEvents)
        {
          TaskHelper.FireAndForget(QueuePositionChange);

          TaskHelper.FireAndForget(() => TriggerTrackingEvent(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
            TrackingEventType.Enter,
            operationDate));
        }

        return element;
      }
    }

    public override void RemoveLastVirtualPosition()
    {
      lock (LockObject)
      {
        var virtualElement = Positions.Where(x => x.IsVirtual).LastOrDefault();

        if (virtualElement != null)
          virtualElement.SetElement(null);

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public override ITrackingInstructionDataContractBase DischargeElement(DateTime operationDate)
    {
      lock (LockObject)
      {
        TrackingCollectionElementAbstractBase result = Positions[PositionsAmount - 1].Element;
        if (Positions[PositionsAmount - 1].Element == null)
        {
          throw new Exception("For DischargeElement operation last element should not be empty");
        }

        if (VirtualPositionsAmount > 0)
          AddElementToVirtualPosition(Positions[PositionsAmount - 1].Element);

        Positions[PositionsAmount - 1].SetElement(null);

        if (result.MaterialInfoCollection != null)
          result.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        TaskHelper.FireAndForget(QueuePositionChange);
        TaskHelper.FireAndForget(() => TriggerTrackingEvent(result.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
          TrackingEventType.Exit,
          operationDate));

        return result;
      }
    }

    public virtual void MoveForward(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (Positions[PositionsAmount - 1].Element != null)
        {
          //if (withAutomaticDischarge)
          //  DischargeElement(operationDate);
          //else
          throw new Exception("For MoveForward operation last element should be empty");
        }

        for (int i = PositionsAmount - 1; i >= 0; i--)
        {
          if (i != 0)
          {
            Positions[i].SetElement(Positions[i - 1].Element);
          }
          else
          {
            // for move forward operation first element should be empty
            Positions[i].SetElement(null);
          }
        }

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public virtual void MoveBackward()
    {
      lock (LockObject)
      {
        if (Positions[0].Element != null)
        {
          throw new Exception("For MoveBackward operation first element should be empty");
        }

        for (int i = 0; i < PositionsAmount; i++)
        {
          if (i != PositionsAmount - 1)
          {
            Positions[i].SetElement(Positions[i + 1].Element);
          }
          else
          {
            // for move backward operation last element should be empty
            Positions[PositionsAmount - 1].SetElement(null);
          }
        }

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public override void RemoveMaterialFromCollection(long rawMaterialId, DateTime operationDate)
    {
      lock (LockObject)
      {
        var positions = Positions
          .Where(x => x.Element != null && x.Element.MaterialInfoCollection[rawMaterialId] != null)
          .ToList();

        if (!positions.Any())
        {
          throw new Exception($"Material: {rawMaterialId} does not exist in AssetCode: {AreaAssetCode}");
        }

        positions.ForEach(x => x.SetElement(null));

        NotificationController.Info($"Material: {rawMaterialId} has been successfully removed from queue {AreaAssetCode}");

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public override ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request)
    {
      var result = new TrackingInstructionDataContractBase();
      switch (request.InstructionType)
      {
        case var instructionType when instructionType == TrackingInstructionType.MoveForward:
          MoveForward(request.OperationDate);
          return result;
        default:
          return base.ProcessInstruction(request);
      }
    }

    public virtual void RemoveMaterialFromPosition(long rawMaterialId, int positionId)
    {
      lock (LockObject)
      {
        TrackingCollectionPositionBase position = Positions
          .FirstOrDefault(p => p.Element.MaterialInfoCollection[rawMaterialId] != null &&
                               p.PositionId == positionId);

        if (position == null)
        {
          throw new Exception($"Material: {rawMaterialId} does not exist in AssetCode: {AreaAssetCode} PositionId: {positionId}");
        }

        position.SetElement(null);
        NotificationController.Info($"Material: {rawMaterialId} has been successfully removed from queue {AreaAssetCode} PositionId: {positionId}");

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }


    public virtual TrackingCollectionElementAbstractBase GetMaterialOnLastPosition()
    {
      lock (LockObject)
      {
        TrackingCollectionPositionBase position = Positions
        .Where(p => p.Element != null)
        .OrderByDescending(p => p.PositionId)
        .FirstOrDefault();

        if (position == null)
        {
          throw new Exception($"There are no materials in collection: {AreaAssetCode}");
        }

        return position.Element;
      }
    }

    public virtual TrackingCollectionElementAbstractBase GetMaterialOnFirstPosition()
    {
      lock (LockObject)
      {
        TrackingCollectionPositionBase position = Positions
        .Where(p => p.Element != null)
        .OrderBy(p => p.PositionId)
        .FirstOrDefault();

        if (position == null)
        {
          throw new Exception($"There are no materials in collection: {AreaAssetCode}");
        }

        return position.Element;
      }
    }

    public virtual MaterialInfoBase GetMaterialByPosition(int positionId)
    {
      lock (LockObject)
      {
        TrackingCollectionPositionBase position = Positions
        .Where(p => p.PositionId == positionId && p.Element != null)
        .OrderBy(p => p.PositionId)
        .FirstOrDefault();

        return position?.Element?.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
      }
    }

    #endregion methods

    #region Protected methods
    protected virtual void InitPositions(List<QueuePosition> positionsToBeInitialized)
    {
      for (int i = 0; i < PositionsAmount; i++)
      {
        int index = i + 1;

        AddPosition(positionsToBeInitialized, index, false);
      }

      for (int i = 0; i < VirtualPositionsAmount; i++)
      {
        int index = PositionsAmount + i + 1;

        AddPosition(positionsToBeInitialized, index, true);
      }
    }

    protected virtual void AddPosition(List<QueuePosition> positionsToBeInitialized, int index, bool isVirtual)
    {
      var positionInDatabase =
                positionsToBeInitialized.FirstOrDefault(q => q.PositionSeq == index && q.IsVirtualPosition == isVirtual);

      var position = new TrackingCollectionPosition(index, AreaAssetCode, isVirtual);

      if (positionInDatabase != null && !positionInDatabase.IsEmpty && positionInDatabase.RawMaterialId.HasValue)
      {
        position.SetElement(new TrackingCollectionElementBase(new MaterialInfo(positionInDatabase.RawMaterialId.Value)));
      }

      Positions.Add(position);
    }

    /// <summary>
    /// </summary>
    /// <remarks>Should be locked outside</remarks>
    /// <param name="dischargedElementAbstract"></param>
    protected virtual void AddElementToVirtualPosition(TrackingCollectionElementAbstractBase dischargedElementAbstract)
    {
      lock (LockObject)
      {
        for (int i = Positions.Count - 1; i >= 0; i--)
        {
          if (!Positions[i].IsVirtual)
          {
            break;
          }

          if (Positions[i].Element != null)
          {
            throw new Exception("For AddElementToVirtualPosition operation last virtual element should be empty");
          }

          if (!Positions[i - 1].IsVirtual)
          {
            Positions[i].SetElement(dischargedElementAbstract);
          }
        }

        TaskHelper.FireAndForget(QueuePositionChange);
      }
    }

    public override ITrackingInstructionDataContractBase GetFirstVirtualPosition()
    {
      lock (LockObject)
      {
        for (int i = 0; i < Positions.Count; i++)
        {
          if (!Positions[i].IsVirtual)
          {
            continue;
          }

          if (Positions[i].Element != null && Positions[i].Element.MaterialInfoCollection.MaterialInfos.FirstOrDefault() is not null)
          {
            return Positions[i].Element;
          }
        }
      }

      throw new Exception("Cannot find virtual position with material");
    }

    public override ITrackingInstructionDataContractBase GetLastVirtualPosition()
    {
      lock (LockObject)
      {
        for (int i = Positions.Count - 1; i >= 0; i--)
        {
          if (!Positions[i].IsVirtual)
          {
            break;
          }

          if (Positions[i].Element != null && Positions[i].Element.MaterialInfoCollection.MaterialInfos.FirstOrDefault() is not null)
          {
            return Positions[i].Element;
          }
        }
      }

      throw new Exception("Cannot find virtual position with material");
    }

    protected override void QueuePositionChange()
    {
      try
      {
        List<QueuePosition> list = new List<QueuePosition>();
        for (int i = 0; i < Positions.Count; i++)
        {
          var position = Positions[i];
          var materialInfo = position.Element?.MaterialInfoCollection?.MaterialInfos?.FirstOrDefault();
          long? materialId = materialInfo?.MaterialId ?? (long?)null;

          list.Add(new QueuePosition(i + 1, 1, AreaAssetCode, materialId, position.IsVirtual,
              materialId.HasValue,
              materialInfo?.GetHistoryItemDateByAreaCodeAndHistoryType(AreaAssetCode, TrackingHistoryTypeEnum.Charge),
              null, ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
        }

        TrackingEventStorageProvider.TrackingQueuePositionChangeEvents.Enqueue(
          new TrackingQueuePositionChangeEventArgs(list, AreaAssetCode));
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          $"Something went wrong while Enqueue to TrackingQueuePositionChangeEvents with parameters: AssetCode: {AreaAssetCode}");
      }
    }

    protected override void TriggerTrackingEvent(long materialId,
      TrackingEventType eventType,
      DateTime triggerDate,
      bool isArea = true,
      int? assetCode = null)
    {
      try
      {
        TrackingEventStorageProvider.TrackingPointEventsToBeProcessed.Enqueue(
          new TrackingEventArgs(materialId,
            assetCode ?? AreaAssetCode,
            isArea,
            eventType,
            triggerDate));
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          $"Something went wrong while Enqueue to TrackingQueuePositionChangeEvents with parameters: AssetCode: {AreaAssetCode}");
      }
    }
    #endregion
  }
}
