using System;
using System.Collections.Concurrent;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.TRK.Base.Models.TrackingEntities.Concrete;

namespace PE.TRK.Base.Providers.Abstract
{
  public interface ITrackingEventStorageProviderBase
  {
    ConcurrentQueue<TrackingEventArgs> TrackingPointEventsToBeProcessed { get; set; }

    ConcurrentQueue<DcRelatedToMaterialMeasurementRequest> MeasurementEventsToBeProcessed { get; set; }
    ConcurrentQueue<DcAggregatedMeasurementRequest> AggregatedMeasurementsToBeProcessed { get; set; }

    ConcurrentQueue<TrackingQueuePositionChangeEventArgs> TrackingQueuePositionChangeEvents { get; set; }

    ConcurrentDictionary<Guid, DcTrackingPointSignal> TrackingPointSignalsToBeProcessed { get; set; }

    int LayingHeadAssetCode { get; set; }
  }
}
