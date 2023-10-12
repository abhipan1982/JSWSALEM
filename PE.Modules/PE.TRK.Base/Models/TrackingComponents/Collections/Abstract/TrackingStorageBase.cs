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
  public abstract class TrackingStorageBase : TrackingCollectionAreaBase
  {
    #region properties
    protected Stack<TrackingCollectionElementAbstractBase> Elements { get; }

    #endregion properties

    #region ctor

    public TrackingStorageBase(
      ITrackingEventStorageProviderBase trackingEventStorageProvider,
      short areaAssetCode,
      List<QueuePosition> positionsToBeInitialized)
      : base(trackingEventStorageProvider, areaAssetCode, false, 0, 0)
    {
      Elements = new Stack<TrackingCollectionElementAbstractBase>();
      InitPositions(positionsToBeInitialized);
    }

    #endregion ctor    

    #region protected methods

    public override void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false)
    {
      lock (LockObject)
      {
        var element = Elements.FirstOrDefault();

        if (element == null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} nothing to drag");

        if (element.MaterialInfoCollection[rawMaterialId] == null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} First element does not contains {rawMaterialId}");

        element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

        if (element.MaterialInfoCollection.MaterialInfos.Count == 0)
          Elements.Pop();

        TaskHelper.FireAndForget(QueuePositionChange);

        if (!isPreviousDragForTheSameAsset)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, TrackingEventType.Exit, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override void Drop(long rawMaterialId, int dropSeq, int? dragSeq)
    {
      lock (LockObject)
      {
        if (Elements.Any(x => x.MaterialInfoCollection[rawMaterialId] != null))
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");

        Elements.Push(new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));

        TaskHelper.FireAndForget(QueuePositionChange);

        if (dragSeq != dropSeq)
          TaskHelper.FireAndForget(() => TriggerTrackingEvent(
            rawMaterialId, TrackingEventType.Enter, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));
      }
    }

    public override ITrackingInstructionDataContractBase ChargeElement(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false)
    {
      lock (LockObject)
      {
        var element = elementAbstract as TrackingCollectionElementAbstractBase;
        Elements.Push(element);

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
        RawMaterialId = p.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
        Order = ++order
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

        TrackingCollectionElementAbstractBase result = Elements.Pop();

        if (result.MaterialInfoCollection != null)
          result.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        TaskHelper.FireAndForget(QueuePositionChange);
        TaskHelper.FireAndForget(() => TriggerTrackingEvent(result.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId,
            TrackingEventType.Exit,
            operationDate));

        return result;
      }
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

        if (elementAbstract.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId != rawMaterialId)
        {
          throw new Exception($"Storage: {AreaAssetCode} does not have rawMaterial: {rawMaterialId} at first position");
        }

        Elements.Pop();

        TaskHelper.FireAndForget(QueuePositionChange);

        NotificationController.Info(
          $"Material: {rawMaterialId} has been successfully removed from storage {AreaAssetCode}");
      }
    }

    public virtual void PasteMaterialToPosition(long rawMaterialId, long positionId)
    {
      lock (LockObject)
      {
        TrackingCollectionElementBase elementAbstract = new TrackingCollectionElementBase();

        elementAbstract.MaterialInfoCollection.MaterialInfos.Add(new MaterialInfo(rawMaterialId));
        Elements.Push(elementAbstract);

        TaskHelper.FireAndForget(QueuePositionChange);

        NotificationController.Info(
          $"Material: {rawMaterialId} has been successfully pasted to queue {AreaAssetCode}");
      }
    }

    public override void RemoveMaterialFromCollection(long rawMaterialId, DateTime operationDate)
    {
      RemoveMaterial(rawMaterialId);
    }

    #endregion protected methods

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
  }
}
