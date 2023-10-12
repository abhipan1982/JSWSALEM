using System.Collections.Generic;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class TrackingHookNonPositionRelatedList
    : TrackingNonPositionRelatedListWithCorrelationIdBase
  {
    public TrackingHookNonPositionRelatedList(ITrackingEventStorageProviderBase trackingEventStorageProvider, int areaAssetCode, List<QueuePosition> positionsToBeInitialized, int positionsAmount, int virtualPositionsAmount)
      : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized, positionsAmount, virtualPositionsAmount)
    {
    }
  }
}
