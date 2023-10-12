using System.Collections.Generic;
using System.Linq;
using PE.TRK.Base.Models.TrackingComponents.Collections.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Abstract;
using PE.TRK.Base.Models.TrackingEntities.Concrete;
using PE.TRK.Base.Providers.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.Collections.Concrete
{
  public class TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionBase
    : TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionAbstractBase
  {
    public TrackingPositionRelatedCollectionWithMultipleMaterialsPerPositionBase(ITrackingEventStorageProviderBase trackingEventStorageProvider,
      int areaAssetCode,
      int positionsAmount,
      int virtualPositionsAmount,
      List<QueuePosition> positionsToBeInitialized)
      : base(trackingEventStorageProvider,
          areaAssetCode,
          positionsAmount,
          virtualPositionsAmount,
          positionsToBeInitialized)
    {
    }

    public override ITrackingInstructionDataContractBase GetFirstVirtualPosition()
    {
      return Positions.FirstOrDefault(p => p.IsVirtual && p.Element is not null)?.Element;
    }

    public override ITrackingInstructionDataContractBase GetLastVirtualPosition()
    {
      return Positions.LastOrDefault(p => p.IsVirtual && p.Element is not null)?.Element;
    }
  }
}
