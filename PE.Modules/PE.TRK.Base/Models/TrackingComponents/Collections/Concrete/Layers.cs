using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.AbstractionModels.L1A;
using PE.BaseModels.ConcreteModels.L1A;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Layers : TrackingNonPositionRelatedQueueBase
  {
    #region ctor

    public Layers(int assetCode, TrackingEventType trackingEventType, List<QueuePosition> positionsToBeInitialized) :
      base(assetCode, trackingEventType)
    {
      InitPositions(positionsToBeInitialized);
    }

    #endregion

    #region members

    private static readonly object _objectLockForL1TrackingQueuePositionChangeEvent = new object();
    private static event L1TrackingQueuePositionChangeEventHandler _l1TrackingQueuePositionChangeEvent;
    private static event SendTrackingEventEventHandler _sendTrackingEvent;
    private static readonly object _sendEventLocker = new object();

    #endregion

    #region events

    /// <summary>
    ///   OnL1TrackingQueuePositionChangeEvent
    /// </summary>
    public static event L1TrackingQueuePositionChangeEventHandler OnL1TrackingQueuePositionChangeEvent
    {
      add
      {
        lock (_objectLockForL1TrackingQueuePositionChangeEvent)
        {
          _l1TrackingQueuePositionChangeEvent += value;
        }
      }
      remove
      {
        lock (_objectLockForL1TrackingQueuePositionChangeEvent)
        {
          _l1TrackingQueuePositionChangeEvent -= value;
        }
      }
    }

    /// <summary>
    ///   OnSendTrackingEvent
    /// </summary>
    public static event SendTrackingEventEventHandler OnSendTrackingEvent
    {
      add
      {
        lock (_sendEventLocker)
        {
          _sendTrackingEvent += value;
        }
      }
      remove
      {
        lock (_sendEventLocker)
        {
          _sendTrackingEvent -= value;
        }
      }
    }

    #endregion

    #region public methods

    public override void ChargeElement(TrackingQueueElementBase element)
    {
      base.ChargeElement(element);

      if (TrackingEventType == TrackingEventType.Both || TrackingEventType == TrackingEventType.HeadEnter)
      {
        TriggerSendTrackingEvent(new TrackingEventEventArgs(element.MaterialInfo?.MaterialId, false, false, 1,
          AssetCode, true, TrackingEventType.HeadEnter, DateTime.Now));
      }

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override TrackingQueueElementBase DischargeElement()
    {
      TrackingQueueElementBase result = base.DischargeElement();

      if (TrackingEventType == TrackingEventType.Both || TrackingEventType == TrackingEventType.TailLeave)
      {
        TriggerSendTrackingEvent(new TrackingEventEventArgs(result.MaterialInfo?.MaterialId, true, false, 1, AssetCode,
          true, TrackingEventType.TailLeave, DateTime.Now));
      }

      TriggerL1TrackingQueuePositionChangeEvent();

      return result;
    }

    public override void RemoveMaterial(long rawMaterialId)
    {
      base.RemoveMaterial(rawMaterialId);

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override void PasteMaterialToPosition(long rawMaterialId, long positionId)
    {
      TrackingQueueElement element = new TrackingQueueElement();

      element.SetMaterialInfo(new CbMaterialInfo(rawMaterialId));
      Elements.Enqueue(element);

      NotificationController.Log(LogLevel.Information,
        $"Material: {rawMaterialId} has been successfully pasted to queue {AssetCode}");

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public bool TransferLayer()
    {
      if (Elements.Where(x => (x.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsFormed).Count() == 2)
      {
        if (Elements.Where(x => (x.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsForming).Count() == 1)
        {
          return true;
        }
      }

      return false;
    }

    #endregion

    #region private methods

    private void TriggerL1TrackingQueuePositionChangeEvent()
    {
      TrackingQueueElementBase[] elementsArray = Elements.ToArray();

      List<QueuePosition> list = new List<QueuePosition>();
      for (int i = 0; i < elementsArray.Length; i++)
      {
        if (elementsArray[i]?.MaterialInfo?.MaterialId != null)
        {
          list.Add(new QueuePosition(i + 1, 1, AssetCode, elementsArray[i].MaterialInfo.MaterialId.Value, false,
            false));
        }
      }

      try
      {
        lock (_objectLockForL1TrackingQueuePositionChangeEvent)
        {
          //NotificationController.Debug("TriggerTrackingPointEvent");
          TrackingHelper.TrackingQueuePositionChangeEvents.Enqueue(new L1TrackingQueuePositionChangeEventArgs(
            list, (short)AssetCode));
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex,
          $"Something went wrong which Enqueue to TrackingQueuePositionChangeEvents with parameters: AssetCode: {AssetCode}");
      }
    }

    private void InitPositions(List<QueuePosition> positionsToBeInitialized)
    {
      if (positionsToBeInitialized.Any())
      {
        foreach (QueuePosition positionToBeInitialized in
          positionsToBeInitialized.OrderByDescending(p => p.PositionSeq))
        {
          if (positionToBeInitialized?.RawMaterialId != null)
          {
            TrackingQueueElementBase materialElement = new TrackingQueueElement();
            CbMaterialInfo materialInfo = new CbMaterialInfo(positionToBeInitialized.RawMaterialId.Value);
            materialInfo.ChangeBarsCount(positionToBeInitialized.MaterialsCount);
            materialInfo.ChangeLayerStatus(positionToBeInitialized.LayerStatus);

            materialElement.SetMaterialInfo(materialInfo);
            ChargeElement(materialElement);
          }
        }
      }
    }

    private void TriggerSendTrackingEvent(TrackingEventEventArgs args)
    {
      _sendTrackingEvent?.Invoke(args);
    }

    public long? FormFinished()
    {
      TrackingQueueElementBase element =
        Elements.FirstOrDefault(x => (x.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsForming);

      if (element != null)
      {
        (element.MaterialInfo as CbMaterialInfo).ChangeLayerStatus(LayerStatus.IsFormed);
      }

      return element?.MaterialInfo?.MaterialId;
    }

    public long IncreaseBarCountForFormingLayer(int count)
    {
      TrackingQueueElementBase element =
        Elements.FirstOrDefault(x => (x.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsForming);
      if (element == null)
      {
        element = Elements.FirstOrDefault(x => (x.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.New);
        (element.MaterialInfo as CbMaterialInfo).ChangeLayerStatus(LayerStatus.IsForming);
      }

      (element.MaterialInfo as CbMaterialInfo).ChangeBarsCount(count);

      return element.MaterialInfo.MaterialId.Value;
    }

    public List<LayerElement> GetLayers()
    {
      int order = 0;
      return Elements.Select(p => new LayerElement
      {
        Id = p.MaterialInfo.MaterialId ?? 0,
        PositionOrder = ++order,
        MaterialsSum = (p.MaterialInfo as CbMaterialInfo).BarsCount,
        IsEmpty = (p.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.New,
        IsForming = (p.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsForming,
        IsFormed = (p.MaterialInfo as CbMaterialInfo).LayerStatus == LayerStatus.IsFormed
      }).ToList();
    }

    #endregion
  }
}
