using System.Collections.Generic;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class TrackingNonPositionRelatedListBase : TrackingNonPositionRelatedListAbstractBase
  {
    public TrackingNonPositionRelatedListBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      List<QueuePosition> positionsToBeInitialized,
      int positionsAmount,
      int virtualPositionsAmount,
      Dictionary<long, short> layerWithMaterialsCounterMap = null)
      : base(trackingEventStorageProvider, areaAssetCode, positionsToBeInitialized, positionsAmount, virtualPositionsAmount, layerWithMaterialsCounterMap)
    {
    }
  }
}
