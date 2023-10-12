using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.AbstractionModels.L1A;
using PE.BaseModels.ConcreteModels.L1A;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Weighing : TrackingQueueBase
  {
    #region ctor

    public Weighing(int queueSize, int assetCode, TrackingEventType trackingEventType,
      List<QueuePositionElementBase> trackingQueuePositions, List<QueuePosition> positionsToBeInitialized,
      int virtualPositionCount = 5) : base(queueSize, virtualPositionCount, assetCode, trackingEventType,
      trackingQueuePositions)
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
      if (Positions[0].Element != null)
      {
        MoveForward();
      }

      base.ChargeElement(element);

      if (TrackingEventType == TrackingEventType.Both || TrackingEventType == TrackingEventType.HeadEnter)
      {
        TriggerSendTrackingEvent(new TrackingEventEventArgs(element.MaterialInfo?.MaterialId, false, false, 1,
          AssetCode, true, TrackingEventType.HeadEnter, DateTime.Now));
      }

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override void RemoveLastVirtualPosition()
    {
      base.RemoveLastVirtualPosition();

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override void MoveBackward()
    {
      base.MoveBackward();

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override void MoveForward()
    {
      base.MoveForward();

      TriggerL1TrackingQueuePositionChangeEvent();
    }

    public override TrackingQueueElementBase DischargeElement()
    {
      TrackingQueueElementBase result = DischargeMaterial();

      if (TrackingEventType == TrackingEventType.Both || TrackingEventType == TrackingEventType.TailLeave)
      {
        TriggerSendTrackingEvent(new TrackingEventEventArgs(result.MaterialInfo?.MaterialId, false, false, 1, AssetCode,
          true, TrackingEventType.TailLeave, DateTime.Now));
      }

      TriggerL1TrackingQueuePositionChangeEvent();

      MoveForward();

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
      try
      {
        lock (_objectLockForL1TrackingQueuePositionChangeEvent)
        {
          //NotificationController.Debug("TriggerTrackingPointEvent");
          TrackingHelper.TrackingQueuePositionChangeEvents.Enqueue(new L1TrackingQueuePositionChangeEventArgs(
            Positions
              .Select(p => new QueuePosition(p.PositionId, 1, AssetCode, p.Element?.MaterialInfo?.MaterialId,
                p.IsVirtual, p.Element == null))
              .ToList(), (short)AssetCode));
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
        foreach (TrackingQueuePosition position in Positions)
        {
          QueuePosition positionToBeInitialized =
            positionsToBeInitialized.FirstOrDefault(p => p.PositionSeq == position.PositionId);

          if (positionToBeInitialized != null && !positionToBeInitialized.IsEmpty)
          {
            position.SetElement(new TrackingQueueElement());

            if (positionToBeInitialized.RawMaterialId.HasValue)
            {
              position.Element.SetMaterialInfo(new MaterialInfo(positionToBeInitialized.RawMaterialId.Value));
            }
            else
            {
              position.Element.SetMaterialInfo(new MaterialInfo());
            }
          }
        }
      }
    }

    private void TriggerSendTrackingEvent(TrackingEventEventArgs args)
    {
      _sendTrackingEvent?.Invoke(args);
    }

    private TrackingQueueElementBase DischargeMaterial()
    {
      lock (LockObject)
      {
        TrackingQueueElementBase result;
        TrackingQueuePosition position = Positions.Where(x => x.Element?.MaterialInfo?.MaterialId != null)
          .OrderByDescending(x => x.PositionId).FirstOrDefault();
        if (position == null)
        {
          throw new Exception("For DischargeElement operation in Weighing Area should be at least one material");
        }

        result = position.Element;
        AddElementToVirtualPosition(position.Element);

        position.SetElement(null);
        return result;
      }
    }

    #endregion
  }
}
