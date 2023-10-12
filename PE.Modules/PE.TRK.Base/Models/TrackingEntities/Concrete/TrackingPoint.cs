using System;
using System.Threading.Tasks;
using PE.BaseDbEntity.EnumClasses;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingEntities
{
  /// <summary>
  ///   Tracking point class
  /// </summary>
  [Serializable]
  public class TrackingPoint : TrackingPointBase
  {
    protected readonly ITrackingEventStorageProviderBase EventStorageProvider;

    #region ctor

    /// <summary>
    ///   ctor
    /// </summary>
    /// <param name="occupiedFeatureCode"></param>
    /// <param name="assetCode"></param>
    /// <param name="eventStorageProvider"></param>
    public TrackingPoint(int occupiedFeatureCode, 
      int assetCode, 
      int stepAssetCode,
      int sequence,
      ITrackingEventStorageProviderBase eventStorageProvider) 
      : base(occupiedFeatureCode, assetCode, stepAssetCode, sequence)
    {
      EventStorageProvider = eventStorageProvider;
    }

    #endregion


    public bool NotOccupiedSignalStateShouldBeAccepted()
    {
      return HeadReceived && !TailReceived;
    }

    /// <summary>
    ///   Change HeadReceived flag
    /// </summary>
    /// <param name="headReceived"></param>
    /// <param name="operationDate"></param>
    /// <param name="materialId"></param>
    public void ChangeHeadReceived(bool headReceived, DateTime? operationDate, long? materialId = null)
    {
      HeadReceived = headReceived;
      HeadReceivedDate = operationDate;
      
      if (headReceived && materialId.HasValue && operationDate.HasValue)
      {
        EventStorageProvider.TrackingPointEventsToBeProcessed
          .Enqueue(new TrackingEventArgs(materialId.Value, AssetCode, false, TrackingEventType.Enter, operationDate.Value));
      }
    }
    
    /// <summary>
    ///   Change TailReceived flag
    /// </summary>
    /// <param name="tailReceived"></param>
    /// <param name="materialId"></param>
    /// <param name="operationDate"></param>
    public virtual void ChangeTailReceived(bool tailReceived, DateTime? operationDate, long? materialId = null)
    {
      TailReceived = tailReceived;
      TailReceivedDate = operationDate;

      if (tailReceived && materialId.HasValue && operationDate.HasValue)
      {
        //Measurement request will be send once material leave the area
        //EventStorageProvider.MeasurementEventsToBeProcessed
        //  .Enqueue(new DcRelatedToMaterialMeasurementRequest(materialId.Value, OccupiedFeatureCode, HeadReceivedDate.Value, TailReceivedDate.Value));
          
        EventStorageProvider.TrackingPointEventsToBeProcessed
          .Enqueue(new TrackingEventArgs(materialId.Value, AssetCode, false, TrackingEventType.Exit, operationDate.Value));
      }
    }

    /// <summary>
    ///   Should signal be ignored by in progress working
    /// </summary>
    /// <param name="trackingPoint"></param>
    /// <returns></returns>
    public bool NotOccupiedSignalShouldBeIgnored(int trackingPoint)
    {
      return AssetCode == trackingPoint && ((HeadReceived && TailReceived) || !HeadReceived);
    }

    /// <summary>
    ///   Should signal be ignored by already done
    /// </summary>
    /// <param name="trackingPoint"></param>
    /// <returns></returns>
    public bool NotOccupiedSignalWasAlreadyDone(int trackingPoint)
    {
      return OccupiedFeatureCode == trackingPoint && HeadReceived && TailReceived;
    }

    public bool OccupiedSignalWasAlreadyDone(short trackingPointAssetCode)
    {
      return OccupiedFeatureCode == trackingPointAssetCode && HeadReceived && TailReceived;
    }

    public bool ActiveMaterial(short trackingPointAssetCode)
    {
      return OccupiedFeatureCode == trackingPointAssetCode && HeadReceived;
    }
  }
}
