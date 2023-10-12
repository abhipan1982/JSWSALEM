using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.HMI;
using PE.Helpers;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Abstract;
using PE.TRK.Base.Models.TrackingComponents.CollectionElements.Concrete;
using PE.TRK.Base.Models.TrackingComponents.MaterialInfos.Concrete;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Abstract
{
  public abstract class TrackingNonPositionRelatedQueueBase : TrackingCollectionAreaBase
  {
    #region properties

    protected Queue<TrackingCollectionElementAbstractBase> Elements { get; }

    #endregion properties

    #region ctor

    protected TrackingNonPositionRelatedQueueBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized)
    : base(trackingEventStorageProvider, areaAssetCode, false, 0, 0)
    {
      Elements = new Queue<TrackingCollectionElementAbstractBase>();

      InitPositions(positionsToBeInitialized);
    }

    #endregion ctor

    public override void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false)
    {
      lock (LockObject)
      {
        var element = Elements.LastOrDefault();

        if (element == null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} nothing to drag");

        if (element.MaterialInfoCollection[rawMaterialId] == null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} Last element does not contains {rawMaterialId}");

        element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

        if (element.MaterialInfoCollection.MaterialInfos.Count == 0)
          Elements.Dequeue();

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
        if (Elements.Any(x => x.MaterialInfoCollection[rawMaterialId] != null))
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");

        Elements.Enqueue(new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));

        TaskHelper.FireAndForget(QueuePositionChange);

        if (dragSeq != dropSeq)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, TrackingEventType.Enter, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override List<long> GetMaterialIds()
    {
      return Elements
        .Concat(Elements)
        .Where(p => p.MaterialInfoCollection.MaterialInfos.Count > 0)
        .SelectMany(p => p.MaterialInfoCollection.MaterialInfos
          .Select(x => x.MaterialId))
        .ToList();
    }

    #region protected methods

    protected virtual void InitPositions(List<QueuePosition> positionsToBeInitialized)
    {
      if (positionsToBeInitialized.Any())
      {
        foreach (QueuePosition positionToBeInitialized in
          positionsToBeInitialized.OrderByDescending(p => p.PositionSeq))
        {
          if (positionToBeInitialized?.RawMaterialId == null)
          {
            continue;
          }

          TrackingCollectionElementAbstractBase materialElement = new TrackingCollectionElementBase();

          materialElement.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(positionToBeInitialized.RawMaterialId.Value));
          ChargeElement(materialElement, DateTime.Now, true);
        }
      }
    }

    public override ITrackingInstructionDataContractBase ChargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false)
    {
      lock (LockObject)
      {
        var element = elementAbstract as TrackingCollectionElementAbstractBase;
        Elements.Enqueue(element);

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

    public override List<MaterialPosition> GetMaterials()
    {
      int order = 0;
      return Elements.Select(p => new MaterialPosition
      {
        RawMaterialId = p.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
        PositionOrder = ++order,
        Order = 1
      }).ToList();
    }

    public override ITrackingInstructionDataContractBase DischargeElement(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (!Elements.Any())
        {
          throw new Exception("There is nothing to discharge - Storage is empty");
        }

        TrackingCollectionElementAbstractBase result = Elements.Dequeue();

        if (result.MaterialInfoCollection != null)
          result.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(result.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
          TrackingEventType.Enter,
          operationDate));

        return result;
      }
    }

    public override void RemoveMaterialFromCollection(long rawMaterialId, DateTime operationDate)
    {
      RemoveMaterial(rawMaterialId);
    }

    public override void RemoveLastVirtualPosition()
    {
    }

    public virtual void RemoveMaterial(long rawMaterialId)
    {
      lock (LockObject)
      {
        TrackingCollectionElementAbstractBase elementAbstract = Elements.FirstOrDefault();

        if (elementAbstract == null)
        {
          throw new Exception($"Storage: {AreaAssetCode} is empty");
        }

        if (elementAbstract.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId != rawMaterialId)
        {
          throw new Exception($"Storage: {AreaAssetCode} does not have rawMaterial: {rawMaterialId} at first position");
        }

        Elements.Dequeue();

        TaskHelper.FireAndForget(QueuePositionChange);

        NotificationController.Info(
          $"Material: {rawMaterialId} has been successfully removed from storage {AreaAssetCode}");
      }
    }

    protected override void QueuePositionChange()
    {
      try
      {
        TrackingCollectionElementAbstractBase[] elementsArray = Elements.ToArray();

        List<QueuePosition> list = new List<QueuePosition>();
        for (int i = 0; i < elementsArray.Length; i++)
        {
          var materialInfo = elementsArray[i]?.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
          if (materialInfo != null)
          {
            list.Add(new QueuePosition(i + 1, 1, AreaAssetCode, materialInfo.MaterialId, false,
              false,
              elementsArray[i].MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.GetHistoryItemDateByAreaCodeAndHistoryType(AreaAssetCode, TrackingHistoryTypeEnum.Charge), 
              null, ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
          }
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

    #endregion protected methods
  }
}
