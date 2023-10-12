using System;
using System.Collections.Generic;
using System.Linq;
using PE.BaseDbEntity.EnumClasses;
using SMF.Core.Notification;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class Garret : TrackingQueueBase
  {
    #region ctor

    public Garret(int queueSize, int assetCode, TrackingEventType trackingEventType,
      List<QueuePositionElementBase> trackingQueuePositions, List<QueuePosition> positionsToBeInitialized,
      int virtualPositionCount = 5) : base(queueSize, virtualPositionCount, assetCode, trackingEventType,
      trackingQueuePositions)
    {
      InitPositions(positionsToBeInitialized);
    }

    #endregion

    #region public methods

    public override void ChargeElement(TrackingQueueElementBase element)
    {
      base.ChargeElement(element);

      if (TrackingEventType == TrackingEventType.Both || TrackingEventType == TrackingEventType.HeadEnter)
      {
        TriggerSendTrackingEvent(new TrackingEventEventArgs(element.MaterialInfo?.MaterialId, true, false, 1, AssetCode,
          true, TrackingEventType.HeadEnter, DateTime.Now));
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

    #endregion
  }
}
