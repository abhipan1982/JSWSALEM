using System;
using System.Collections.Generic;
using System.Linq;
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
  public abstract class TrackingNonPositionRelatedListAbstractBase : TrackingCollectionAreaBase
  {
    #region properties
    protected readonly Dictionary<long, short> LayerWithMaterialsCounterMap = new();
    protected List<TrackingCollectionElementAbstractBase> Elements { get; } = new List<TrackingCollectionElementAbstractBase>();
    protected List<TrackingCollectionElementAbstractBase> VirtualElements { get; } = new List<TrackingCollectionElementAbstractBase>();

    #endregion properties

    #region ctor

    protected TrackingNonPositionRelatedListAbstractBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount,
      Dictionary<long, short> layerWithMaterialsCounterMap = null)
    : base(trackingEventStorageProvider, areaAssetCode, false, positionsAmount, virtualPositionsAmount)
    {
      LayerWithMaterialsCounterMap = layerWithMaterialsCounterMap;
      InitPositions(positionsToBeInitialized);
    }

    #endregion ctor    

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

          var materialInfo = new MaterialInfo(positionToBeInitialized.RawMaterialId.Value);

          if (!string.IsNullOrWhiteSpace(positionToBeInitialized.CorrelationId))
          {
            if(int.TryParse(positionToBeInitialized.CorrelationId, out int correlationId))
            {
              materialInfo.ChangeCorrelationId(new IntCorrelationId(correlationId));
            }
          }
          
          materialElement.MaterialInfoCollection.MaterialInfos.Add(materialInfo);

          if (positionToBeInitialized.IsVirtualPosition)
          {
            VirtualElements.Add(materialElement);
          }
          else
          {
            ChargeElement(materialElement, DateTime.Now, true);
          }
        }
      }
    }

    protected override void QueuePositionChange()
    {
      try
      {
        TrackingCollectionElementAbstractBase[] elementsArray = Elements.ToArray();
        var virtualElements = VirtualElements.AsReadOnly();

        List<QueuePosition> list = new List<QueuePosition>();
        for (int i = 0; i < elementsArray.Length; i++)
        {
          var materialInfo = elementsArray[i]?.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
          if (materialInfo != null)
          {
            list.Add(new QueuePosition(i + 1, 1, AreaAssetCode, materialInfo.MaterialId, false,
              false,
              elementsArray[i].MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.GetHistoryItemDateByAreaCodeAndHistoryType(AreaAssetCode, TrackingHistoryTypeEnum.Charge), null, ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
          }
        }

        int index = elementsArray.Length;
        foreach (var virtualElement in virtualElements)
        {
          var materialInfo = virtualElement.MaterialInfoCollection.MaterialInfos.FirstOrDefault();
          if (materialInfo != null)
          {
            list.Add(new QueuePosition(++index, 1, AreaAssetCode, materialInfo.MaterialId, true,
              false, null, null, ((materialInfo.CorrelationId as IntCorrelationId)?.Value)?.ToString()));
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

    protected virtual List<TrackingCollectionElementAbstractBase> RemoveMaterialFromVirtualElements(long rawMaterialId)
    {
      List<TrackingCollectionElementAbstractBase> positions = VirtualElements
                .Where(x => x.MaterialInfoCollection[rawMaterialId] != null)
                .ToList();

      if (positions.Any())
      {
        positions.ForEach(x => VirtualElements.Remove(x));

        TaskHelper.FireAndForget(QueuePositionChange);
      }

      return positions;
    }

    protected virtual List<TrackingCollectionElementAbstractBase> RemoveMaterialFromElements(long rawMaterialId)
    {
      var positions = Elements
                .Where(x => x.MaterialInfoCollection[rawMaterialId] != null)
                .ToList();

      if (positions.Any())
      {
        positions.ForEach(x => Elements.Remove(x));

        TaskHelper.FireAndForget(QueuePositionChange);
      }

      return positions;
    }

    protected virtual ITrackingInstructionDataContractBase ProcessChargeWithPreventOverflowInstruction(ITrackingInstructionRequest request)
    {
      if (request.PreviousOperationResult == null)
        throw new ArgumentException($"For instruction type {request.InstructionType} PreviousOperationResult should not be null. Area: {AreaAssetCode}");

      return ChargeElementWithPreventOverflow(request.PreviousOperationResult, request.OperationDate);
    }

    #endregion protected methods

    #region public methods  

    public override ITrackingInstructionDataContractBase ProcessInstruction(ITrackingInstructionRequest request)
    {
      switch (request.InstructionType)
      {
        case var instructionType when instructionType == TrackingInstructionType.ChargeWithPreventOverflow:
          return ProcessChargeWithPreventOverflowInstruction(request);
        default:
          return base.ProcessInstruction(request);
      }
    }

    public override List<long> GetMaterialIds()
    {
      return Elements
        .Concat(VirtualElements)
        .Where(p => p.MaterialInfoCollection.MaterialInfos.Count > 0)
        .SelectMany(p => p.MaterialInfoCollection.MaterialInfos
          .Select(x => x.MaterialId))
        .ToList();
    }

    public override List<MaterialPosition> GetMaterials()
    {
      int order = 0;
      List<MaterialPosition> returnList = new List<MaterialPosition>();
      returnList = Elements.Select(p => new MaterialPosition
      {
        RawMaterialId = p.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
        PositionOrder = ++order,
        Order = 1
      }).ToList();

      foreach (var virtualElement in VirtualElements)
      {
        returnList.Add(new MaterialPosition
        {
          RawMaterialId = virtualElement.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
          PositionOrder = ++order,
          Order = 1,
          IsVirtual = true
        });
      }

      return returnList;
    }

    public override void Drag(long rawMaterialId, int dragSeq, bool isPreviousDragForTheSameAsset, bool dragFromVirtualPosition = false)
    {
      lock (LockObject)
      {
        if(dragFromVirtualPosition)
        {
          if (dragSeq > VirtualElements.Count)
            throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} is OutOfRange. Amount of virtual elements: {VirtualElements.Count}");

          var element = VirtualElements.ElementAt(dragSeq - 1);

          if (element == null || element.MaterialInfoCollection.MaterialInfos.Count == 0)
            throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} nothing to drag");

          element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

          if (element.MaterialInfoCollection.MaterialInfos.Count == 0)
            VirtualElements.Remove(element);
        }
        else
        {
          if (isPreviousDragForTheSameAsset)
            dragSeq++;

          if (dragSeq > Elements.Count)
            throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} is OutOfRange. Amount of elements: {Elements.Count}");

          var element = Elements.ElementAt(dragSeq - 1);

          if (element == null || element.MaterialInfoCollection.MaterialInfos.Count == 0)
            throw new InvalidOperationException($"For area: {AreaAssetCode} dragSeq: {dragSeq} nothing to drag");

          element.MaterialInfoCollection.RemoveMaterial(rawMaterialId);

          if (element.MaterialInfoCollection.MaterialInfos.Count == 0)
            Elements.Remove(element);
        }

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
        var elementsToCheck = Elements;

        if (dragSeq.HasValue && dragSeq <= Elements.Count)
        {
          var elementToExcludeCheck = Elements.ElementAt(dragSeq.Value - 1);
          elementsToCheck = Elements.Where(x => x != elementToExcludeCheck).ToList();
        }

        if (dropSeq == 0 || dropSeq > Elements.Count)
        {
          if (elementsToCheck.Any(x => x.MaterialInfoCollection[rawMaterialId] != null))
          {
            throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");
          }

          Elements.Add(new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));

          TaskHelper.FireAndForget(QueuePositionChange);

          if (dragSeq != dropSeq)
            TaskHelper.FireAndForget(() => TriggerTrackingEvent(
              rawMaterialId, TrackingEventType.Enter, DateTime.Now.ExcludeMiliseconds(), true, AreaAssetCode));

          return;
        }

        var element = Elements.ElementAt(dropSeq - 1);

        if (elementsToCheck.Where(x => x != element).Any(x => x.MaterialInfoCollection[rawMaterialId] != null))
        {
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");
        }

        if (element != null && element.MaterialInfoCollection[rawMaterialId] != null)
          throw new InvalidOperationException($"For area: {AreaAssetCode} material with id: {rawMaterialId} already exist");

        Elements.Insert(dropSeq - 1, new TrackingCollectionElementBase(new MaterialInfo(rawMaterialId)));

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
        if (Elements.Any(x => x.MaterialInfoCollection.Equals(element.MaterialInfoCollection)))
        {
          throw new Exception($"Material: {element.MaterialInfoCollection.MaterialInfos.FirstOrDefault().MaterialId} already exist in AreaAssetCode: {AreaAssetCode}");
        }

        Elements.Insert(0, element);

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

    public virtual ITrackingInstructionDataContractBase ChargeElementWithPreventOverflow(ITrackingInstructionDataContractBase elementAbstract, DateTime operationDate, bool ignoreEvents = false)
    {
      if (Elements.Count != 0 && Elements.Count >= PositionsAmount)
        DischargeElement(operationDate);

      return ChargeElement(elementAbstract, operationDate, ignoreEvents);
    }

    public override ITrackingInstructionDataContractBase DischargeElement(DateTime operationDate)
    {
      lock (LockObject)
      {
        if (!Elements.Any() && !VirtualElements.Any())
        {
          throw new Exception($"There is nothing to Discharge - List is empty for area: {AreaAssetCode}");
        }

        TrackingCollectionElementAbstractBase elementAbstract = Elements.LastOrDefault();

        if (elementAbstract == null)
        {
          NotificationController.Error($"Ignored Discharge operation due to missing element in List for area: {AreaAssetCode}");
          return VirtualElements.Last();
        }

        if (VirtualPositionsAmount > 0)
          VirtualElements.Add(elementAbstract);

        if (elementAbstract.MaterialInfoCollection != null)
          elementAbstract.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        Elements.Remove(elementAbstract);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(elementAbstract.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
          TrackingEventType.Exit,
          operationDate));

        return elementAbstract;
      }
    }

    public ITrackingInstructionDataContractBase DischargeElement(DateTime operationDate, TrackingCollectionElementAbstractBase element)
    {
      lock (LockObject)
      {
        if (!Elements.Any() && !VirtualElements.Any())
        {
          throw new Exception($"There is nothing to Discharge - List is empty for area: {AreaAssetCode}");
        }

        if (element.MaterialInfoCollection != null)
          element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.AddHistoryItem(AreaAssetCode, operationDate, TrackingHistoryTypeEnum.Discharge);

        Elements.Remove(element);

        TaskHelper.FireAndForget(QueuePositionChange);

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(element.MaterialInfoCollection.MaterialInfos.FirstOrDefault()?.MaterialId ?? 0,
          TrackingEventType.Exit,
          operationDate));

        return element;
      }
    }

    public override void RemoveMaterialFromCollection(long rawMaterialId, DateTime operationDate)
    {
      lock (LockObject)
      {
        if (!Elements.Any() && !VirtualElements.Any())
        {
          throw new Exception("There is nothing to Remove - List is empty");
        }

        List<TrackingCollectionElementAbstractBase> positions = RemoveMaterialFromElements(rawMaterialId);
        positions = RemoveMaterialFromVirtualElements(rawMaterialId);

        NotificationController.Info($"Material: {rawMaterialId} has been successfully removed from queue {AreaAssetCode}");

        TaskHelper.FireAndForget(() => TriggerTrackingEvent(rawMaterialId,
          TrackingEventType.Exit,
          operationDate));
      }
    }

    public override void RemoveLastVirtualPosition()
    {
      lock (LockObject)
      {
        var virtualElement = VirtualElements.LastOrDefault(x => x.MaterialInfoCollection is not null);

        if (virtualElement != null)
        {
          VirtualElements.Remove(virtualElement);

          TaskHelper.FireAndForget(QueuePositionChange);
        }
      }
    }

    public override ITrackingInstructionDataContractBase GetFirstVirtualPosition()
    {
      return VirtualElements.FirstOrDefault(x => x.MaterialInfoCollection is not null);
    }

    public override ITrackingInstructionDataContractBase GetLastVirtualPosition()
    {
      return VirtualElements.LastOrDefault(x => x.MaterialInfoCollection is not null);
    }

    public virtual TrackingCollectionElementAbstractBase GetLastElement()
    {
      return Elements.LastOrDefault();
    }

    public virtual TrackingCollectionElementAbstractBase GetFirstElement()
    {
      return Elements.FirstOrDefault();
    }

    public int ElementsCount() => Elements.Count;

    public IEnumerable<TrackingCollectionElementAbstractBase> GetLastElements(int count)
    {
      return Elements.TakeLast(count);
    }

    #endregion
  }
}
