using System;
using System.Collections.Generic;
using PE.BaseDbEntity.EnumClasses;
using PE.TRK.Base.Models.TrackingComponents.Consumption.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities.Concrete
{
  public enum FurnaceOperation
  {
    DischargeToMill,
    DischargeToRail,
    Charge,
    MoveForward,
    MoveBackward
  }

  public enum QueueOperationEnum
  {
    Charge,
    Discharge,
    MoveForward,
    MoveBackward
  }

  public class DcQueueOperation
  {
    public DcQueueOperation(QueueOperationEnum queueOperationType, DateTime operationDate)
    {
      QueueOperationType = queueOperationType;
      OperationDate = operationDate;
    }
    public QueueOperationEnum QueueOperationType { get; private set; }
    public DateTime OperationDate { get; private set; }
  }

  public enum SimpleQueueOperation
  {
    Charge,
    Discharge
  }

  public delegate void L1GetMaterialInfoEventHandler(EventArgs eventArgs);

  public delegate void L1TrackingPointEventHandler(TrackingPointEventArgs eventArgs);

  public delegate void L1TrackingQueuePositionChangeEventHandler(TrackingQueuePositionChangeEventArgs eventArgs);

  public delegate void FurnaceEventHandler(FurnaceEventArgs eventArgs);

  public delegate void SimpleEventHandler(EventArgs eventArgs);

  public delegate void BarsAmountChangedEventHandler(BarsAmountChangeEventArgs eventArgs);


  public delegate void MeasurementReadyEventHandler(MeasurementReadyEventArgs eventArgs);

  public delegate void SimpleQueueEventHandler(SimpleQueueEventArgs eventArgs);

  public delegate void L1ConsumptionMeasurementEventHandler(L1ConsumptionMeasurementEventArgs eventArgs);

  public class FurnaceEventArgs : EventArgs
  {
    public FurnaceEventArgs()
    {
      EventTypes = new List<FurnaceOperation>();
    }

    public List<FurnaceOperation> EventTypes { get; set; }
  }

  public class TrackingQueuePositionChangeEventArgs : EventArgs
  {
    public TrackingQueuePositionChangeEventArgs(List<QueuePosition> queuePositions, int assetCode)
    {
      QueuePositions = queuePositions;
      AssetCode = assetCode;
    }

    public List<QueuePosition> QueuePositions { get; }
    public int AssetCode { get; }
  }

  public class BarsAmountChangeEventArgs : EventArgs
  {
    public BarsAmountChangeEventArgs(short barsAmount)
    {
      BarsAmount = barsAmount;
    }

    public short BarsAmount { get; }
  }

  public class MeasurementReadyEventArgs : EventArgs
  {
    public MeasurementReadyEventArgs(int trackPoint, int measurementId, double value, int positionId,
      DateTime measurementDate, TrackingEventType trackingEventType = null, bool isArea = false)
    {
      trackingEventType = trackingEventType ?? TrackingEventType.Enter;
      TrackPoint = trackPoint;
      PositionId = positionId;
      MeasurementValue = value;
      MeasurementId = measurementId;
      MeasurementDate = measurementDate;
      IsArea = isArea;
      TrackingEventType = trackingEventType;
    }

    public int MeasurementId { get; }
    public double MeasurementValue { get; }
    public int TrackPoint { get; }
    public int PositionId { get; }
    public DateTime MeasurementDate { get; }
    public bool IsArea { get; }
    public TrackingEventType TrackingEventType { get; }
  }

  public class SimpleQueueEventArgs : EventArgs
  {
    public SimpleQueueOperation EventType { get; set; }
  }

  public class TrackingPointEventArgs : EventArgs
  {
    public TrackingPointEventArgs(short trackPoint, DateTime timeOfEvent, bool value)
    {
      TrackPoint = trackPoint;
      TimeOfEvent = timeOfEvent;
      Value = value;
    }

    public short TrackPoint { get; }
    public DateTime TimeOfEvent { get; }
    public bool Value { get; }
  }

  public class L1ConsumptionMeasurementEventArgs : EventArgs
  {
    public L1ConsumptionMeasurementEventArgs(ConsumptionBase millConsumptionMeasurement)
    {
      MillConsumptionMeasurement = millConsumptionMeasurement;
    }

    public ConsumptionBase MillConsumptionMeasurement { get; }
  }

  [Serializable]
  public class QueuePosition
  {
    public int PositionSeq { get; set; }
    public int OrderSeq { get; set; }
    public int AssetCode { get; set; }
    public long? RawMaterialId { get; set; }
    public bool IsVirtualPosition { get; set; }
    public bool IsEmpty { get; set; }
    public int? CtrAssetCode { get; set; }
    public string CorrelationId { get; set; }

    public DateTime? ChargeDate { get; set; }

    #region ctor

    public QueuePosition()
    {
    }

    public QueuePosition(int positionSeq, 
      int orderSeq, 
      int assetCode, 
      long? rawMaterialId, 
      bool isVirtualPosition,
      bool isEmpty, 
      DateTime? chargeDate = null, 
      int? ctrAssetCode = null,
      string correlationId = null)
    {
      PositionSeq = positionSeq;
      OrderSeq = orderSeq;
      AssetCode = assetCode;
      RawMaterialId = rawMaterialId;
      IsVirtualPosition = isVirtualPosition;
      IsEmpty = isEmpty;
      CtrAssetCode = ctrAssetCode;
      ChargeDate = chargeDate;
      CorrelationId = correlationId;
    }

    #endregion ctor
  }
}
