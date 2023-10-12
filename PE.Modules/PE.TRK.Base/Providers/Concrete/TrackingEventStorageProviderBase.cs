using System;
using System.Collections.Concurrent;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Providers.Concrete
{
  public class TrackingEventStorageProviderBase : ITrackingEventStorageProviderBase
  {
    public TrackingEventStorageProviderBase()
    {
      TrackingPointEventsToBeProcessed = new ConcurrentQueue<TrackingEventArgs>();
      TrackingQueuePositionChangeEvents = new ConcurrentQueue<TrackingQueuePositionChangeEventArgs>();
      TrackingPointSignalsToBeProcessed = new ConcurrentDictionary<Guid, DcTrackingPointSignal>();
      MeasurementEventsToBeProcessed = new ConcurrentQueue<DcRelatedToMaterialMeasurementRequest>();
      AggregatedMeasurementsToBeProcessed = new ConcurrentQueue<DcAggregatedMeasurementRequest>();
    }

    public ConcurrentQueue<TrackingEventArgs> TrackingPointEventsToBeProcessed { get; set; }

    public ConcurrentQueue<DcRelatedToMaterialMeasurementRequest> MeasurementEventsToBeProcessed { get; set; }

    public ConcurrentQueue<DcAggregatedMeasurementRequest> AggregatedMeasurementsToBeProcessed { get; set; }

    public ConcurrentQueue<TrackingQueuePositionChangeEventArgs> TrackingQueuePositionChangeEvents { get; set; }

    public ConcurrentDictionary<Guid, DcTrackingPointSignal> TrackingPointSignalsToBeProcessed { get; set; }

    public int LayingHeadAssetCode { get; set; }
  }
}
