using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.AbstractionModels.L1A;
using PE.BaseModels.ConcreteModels.L1A;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Storage : TrackingStorageBase
  {
    #region ctor

    public Storage(int assetCode, TrackingEventType trackingEventType, List<QueuePosition> positionsToBeInitialized) :
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
        TriggerSendTrackingEvent(new TrackingEventEventArgs(result.MaterialInfo?.MaterialId, false, false, 1, AssetCode,
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
      base.PasteMaterialToPosition(rawMaterialId, positionId);

      TriggerL1TrackingQueuePositionChangeEvent();
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

            materialElement.SetMaterialInfo(new MaterialInfo(positionToBeInitialized.RawMaterialId.Value));
            ChargeElement(materialElement);
          }
        }
      }
    }

    private void TriggerSendTrackingEvent(TrackingEventEventArgs args)
    {
      _sendTrackingEvent?.Invoke(args);
    }

    #endregion
  }
}
